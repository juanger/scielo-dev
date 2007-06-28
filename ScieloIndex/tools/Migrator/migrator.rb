#!/usr/bin/env ruby
RAILS_ENV = 'development'
$KCODE='u'

require File.dirname(__FILE__) + '/../../config/environment'
require 'sgmlarticle'
require 'associated_authors'
require 'associated_references'
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
    process_articles(issue_dir)
  end

  def process_articles(issue_dir)
    article_dir = File.join(issue_dir, "markup")

    if File.directory? article_dir
      Dir.foreach(article_dir) { |article|
        next unless article =~ /^.*(\.)(txt)$/

        full_dir = File.join(article_dir, article)
        if File.file? full_dir
          @current_article = article.sub(/\.(txt)/, "")

          puts "Procesando articulo: " + @current_article
          process_article(full_dir)
        end
      }
    else
      puts "El numero no contiene archivos marcados."
    end
  end

  def process_article(marked_file)
    begin
      article = SgmlArticle.new(marked_file)
      puts ""
      puts "Lenguaje: #{article.language}, Titulo Revista: #{article.journal_title}"
      puts "Volumen: #{article.volume}, Numero: #{article.number}"
      puts "Año: #{article.year}, Revista ISSN: #{article.journal_issn}"
      puts "Primera pagina: #{article.fpage}, Ultima pagina: #{article.lpage}"
      puts ""

      if !@current_journal_id
        create_journal(article)
      end

      if !@current_journal_issue_id && @current_journal_id
        create_journal_issue(article)
      end

      if @current_journal_issue_id && @current_journal_id
        create_article(article)
      end
    rescue ArgumentError
      puts "Archivo #{marked_file} no es un article."
    end
  end

  def create_journal(article)
    journal = Journal.new
    journal.title = article.journal_title
    journal.country_id = @default_country_id
    journal.publisher_id = @current_publisher_id
    journal.abbrev = article.journal_title
    journal.issn = article.journal_issn
    journal.incomplete = true

    puts "Titulo de la Revista: #{journal.title}"
    puts "ID del pais: #{journal.country_id}"
    puts "ID del publicador: #{journal.publisher_id}"
    puts "Abreviacion: #{journal.abbrev}"
    puts "ISSN: #{journal.issn}"
    puts ""

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

  def create_journal_issue(article)
    journal_issue = JournalIssue.new
    journal_issue.journal_id = @current_journal_id
    journal_issue.number = article.number
    journal_issue.volume = article.volume
    journal_issue.volume_supplement = article.volume_supplement
    journal_issue.number_supplement = article.number_supplement
    journal_issue.year = article.year

    puts ""
    puts "ID Revista: #{journal_issue.journal_id}"
    puts "Numero: #{journal_issue.number}"
    puts "Volumen: #{journal_issue.volume}"
    puts "Volumen Suplemento: #{journal_issue.volume_supplement}"
    puts "Numero Suplemento: #{journal_issue.number_supplement}"
    puts "Año: #{journal_issue.year}"
    puts ""

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

  def create_article(article)
    new_article = Article.new
    new_article.title = article.title
    new_article.journal_issue_id = @current_journal_issue_id
    new_article.fpage = article.fpage
    new_article.lpage = article.lpage

    puts "Tituto Articulo: #{new_article.title}"
    puts "Titulo Revista: #{new_article.journal_issue.journal.title}"
    puts "Pagina inicial: #{new_article.fpage}, Pagina final: #{new_article.lpage}"

    if new_article.save
      puts "Creando articulo: #{new_article.id}"
      authors = AssociatedAuthors.new(article.front, new_article.id)

      #TODO: Si no hay autores no se crea las referencias asociadas al articulo.
      references = AssociatedReferences.new(article.back, new_article.id, @default_country_id, @current_publisher_id)
      begin
        authors.insert_authors()
        references.insert_references()
      rescue ArgumentError
        puts 'El articulo no tiene autores.'
      end
    else
      puts "Error: #{new_article.errors[:journal_issue_id]}"
      puts "Error: #{new_article.errors[:title].to_s}"
      puts "Error: #{new_article.errors[:fpage].to_s}"
      puts "Error: #{new_article.errors[:lpage].to_s}"
    end
  end
end

migrator = Migrator.new()
migrator.process_scielo()
