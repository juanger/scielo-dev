#!/usr/bin/env ruby
RAILS_ENV = 'development'
require File.dirname(__FILE__) + '/../../config/environment'
require 'sgmlarticle'

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

  def process_journal(journal_dir)
    puts "Migrando la revista: " + @current_journal

    # TODO: Obtener el publisher de la revista
    @current_publisher = default_publisher()
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
    if !@current_journal_id
      marked_file = File.join(@current_issue_full_dir, "markup")
      marked_file = File.join(marked_file, @current_article + ".txt")

      if File.exists? marked_file
        begin
          article = SgmlArticle.new(marked_file)
          puts "Lenguaje: #{article.language}, Titulo: #{article.title}"
          create_journal(marked_file)
        rescue TypeError
          puts "Archivo #{marked_file} no es un article."
        end
      else
        puts "Error: No existe el archivo: #{marked_file}"
      end
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
