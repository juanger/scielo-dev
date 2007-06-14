#!/usr/bin/env ruby
RAILS_ENV = 'development'
$KCODE='u'
require File.dirname(__FILE__) + '/../../config/environment'
require 'sgmlarticle'
require 'jcode'
require 'iconv'


class Migrator
  def initialize
    config = "./config"
    if File.file? config
      open(config, "r") { |file|
        file.each { |line|
          next if line =~ /^#.*$/
          obtain_value line}
      }
    else
      puts "Por favor crear un archivo config."
    end
  end

  def process_scielo
    if File.directory? @serial_root
      Dir.foreach(@serial_root) { |dir|
                next if dir =~ /^(\.|code|issue|issn|section|title|titleanterior)\.?$/

                full_dir = File.join(@serial_root, dir)
                if File.directory? full_dir
                  @current_journal = dir
                  process_journal(full_dir)
                end
      }
    end
  end

  private

  def obtain_value(line)
    array = line.split(':')
    case array[0]
      when 'SERIAL_ROOT'
                @serial_root = array[1].strip
      when 'COUNTRY'
                @default_country_id = get_country_id(array[1].strip)
      else
                puts 'Archivo de configuración ilegal'
    end
  end

  def get_country_id(name)
    country = Country.find_by_name(name)

    #TODO: Lanzar un error si no se encuentra en la DB el pais requerido.
    puts "Country ID por default: #{country.id}"

    country.id
  end

  def default_publisher
    publisher = Publisher.find_by_name("Unassigned")
    if publisher == nil
      publisher = Publisher.new
      publisher.name = "Unassigned"
      publisher.save
    end

    puts "Publisher id: #{publisher.id}"

    publisher.id
  end

  def process_journal(journal_dir)
    puts "Migrando la revista: " + @current_journal

    # TODO: Obtener el publisher de la revista
    @current_publisher_id = default_publisher()
    @current_journal_id = nil

    if File.directory? journal_dir
      Dir.foreach(journal_dir) { |issue_dir|
                next if issue_dir =~ /^(\.|_notes|paginasinformativas)\.?$/

                full_dir = File.join(journal_dir, issue_dir)
                if File.directory? full_dir
                  @current_issue_full_dir = full_dir
                  @current_issue = issue_dir
                  process_issue(full_dir)
                end
      }
    end
  end

  def process_issue(issue_dir)
    puts "Migrando numero: " + @current_issue
    @current_journal_issue_id = nil
    #TODO: Encontrar una forma de llegar al título completo de la revista
    process_pdf(issue_dir)
  end

  def process_pdf(issue_dir)
    pdf_dir = File.join(issue_dir, "pdf")

    if File.directory? pdf_dir
      Dir.foreach(pdf_dir) { |pdf|
        full_dir = File.join(pdf_dir, pdf)
        next unless pdf =~ /^.*(\.)(pdf|PDF)$/

        if File.file? full_dir
          @current_article = pdf.sub(/\.(pdf|PDF)/, "")

          puts "Procesando articulo: " + @current_article
          process_article(full_dir)
        end
      }
    else
      puts "El numero no contiene documentos PDF"
    end
  end

  def process_article(full_path)
    marked_file = File.join(@current_issue_full_dir, "markup")
    marked_file = File.join(marked_file, @current_article + ".txt")

    if File.exists? marked_file
        begin
          article = SgmlArticle.new(marked_file)
          puts ""
          puts "Lenguaje: #{article.language}, Titulo Revista: #{article.journal_title}"
          puts "Volumen: #{article.volume}, Numero: #{article.number}"
          puts "Año: #{article.year}, Revista ISSN: #{article.journal_issn}"
          puts "Primera pagina: #{article.fpage}, Ultima pagina: #{article.lpage}"
          puts ""

          if !@current_journal_id
            journal = Journal.new
            journal.title = article.journal_title
            journal.country_id = @default_country_id
            journal.publisher_id = @current_publisher_id
            journal.abbrev = article.journal_title
            journal.issn = article.journal_issn

            puts "Journal Title: #{journal.title}"
            puts "Journal Country ID: #{journal.country_id}"
            puts "Journal Publisher ID: #{journal.publisher_id}"
            puts "Journal Abbrev: #{journal.abbrev}"
            puts "Journal ISSN: #{journal.issn}"

            if journal.save
              @current_journal_id = journal.id
              puts "Creando journal #{@current_journal_id}"
            else
              puts "Error: #{journal.errors[:title].to_s}"
              puts "Error: #{journal.errors[:country_id].to_s}"
              puts "Error: #{journal.errors[:publisher_id].to_s}"
              puts "Error: #{journal.errors[:abbrev].to_s}"
              puts "Error: #{journal.errors[:issn].to_s}"
            end
          end

          if !@current_journal_issue_id && @current_journal_id
            journal_issue = JournalIssue.new
            journal_issue.journal_id = @current_journal_id
            journal_issue.number = article.number
            journal_issue.volume = article.volume
            journal_issue.supplement =article.supplement
            journal_issue.year = article.year

            if journal_issue.save
              @current_journal_issue_id = journal_issue.id
              puts "Creando journal issue #{@current_journal_issue_id}"
            else
              puts "Error: #{journal_issue.errors[:journal_id].to_s}"
              puts "Error: #{journal_issue.errors[:number].to_s}"
              puts "Error: #{journal_issue.errors[:volume].to_s}"
              puts "Error: #{journal_issue.errors[:supplement].to_s}"
              puts "Error: #{journal_issue.errors[:year].to_s}"
            end
          end

        rescue ArgumentError
          puts "Archivo #{marked_file} no es un article."
        end
    else
      puts "Error: No existe el archivo: #{marked_file}"
    end
  end

  def create_journal(file_dir)
    puts 'Creando registro del journal'
    @current_journal_id = 1
  end
end

# Country.find(:all).each { |country|
#   puts "Id: #{country.id}, Nombre: #{country.name}, Code: #{country.code}"
# }

# Publisher.find(:all).each { |publisher|
#   puts "id: #{publisher.id}, Titulo: #{publisher.name}"
# }

# Journal.find(:all).each { |journal|
#   puts "id: #{journal.id}, Title: #{journal.title}, Country: #{journal.country.name}, Publisher: #{journal.publisher.name}, Abbrev: #{journal.abbrev}, ISSN: #{journal.issn}"
# }

migrator = Migrator.new()
migrator.process_scielo()
