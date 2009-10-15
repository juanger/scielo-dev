#!/usr/bin/env ruby
# encoding: utf-8

#
# == Synopsis 
#   Migrator to populate ScieloIndex database from markup files
#
# == Examples
#   To migrate all the files from the serial tree.
#     migrator.rb --all
#
#   Other examples:
#     migrator.rb --quiet --all
#
# == Usage 
#   migrator.rb [options]
#
#   For help use: migrator -h
#
# == Options
#   -h, --help          Displays help message
#   -v, --version       Display the version, then exit
#   -q, --quiet         Don't display the output
#   -l, --level LEVEL   Override logger level (LEVEL = debug|info|warning|none)
#   -s, --serial SERIAL Path to the serial root directory
#   -a, --all           Migrate all the files
#   -o, --only-new      Migrate only the new files reported by git
#
# == Authors
#   Juan Germán Castañeda Echevarría (juanger)
#   Héctor Enrique Gómez Morales (hectoregm)
#   Alejandro Eduardo Cruz Paz (vidriloco)

RAILS_ENV = 'development'

require File.join(File.dirname(__FILE__), '../../config/environment')

$KCODE='u'
require 'jcode'
require 'iconv'
require 'optparse'
require 'ostruct'
require 'rdoc/usage'

MIGRATOR_ROOT = File.join(RAILS_ROOT,'tools','Migrator')

$: << File.expand_path(MIGRATOR_ROOT)

require 'sgmlarticle'
require 'associated_authors'
require 'associated_references'
require 'associated_files'
require 'statistic'
require 'mylogger'


class Migrator
  VERSION = '2.0'
  
  def initialize(args)
    config = File.join(MIGRATOR_ROOT,"config")
    # Default options
    @options = OpenStruct.new(:level => 'info', :verbose => true, :all => true)
    @default_country = 'México'
    @default_country_id = get_country_id(@default_country)
    # Get config file options
    if File.file? config
      open(config, "r") { |file|
        file.each { |line|
          next if line =~ /^(#.*)?$/
          obtain_value line}
      }
    else
      puts "[WARNING] No se encontró un archivo de configuración para el migrador"
    end
    
    # Override options with command line args
     opts = OptionParser.new
     opts.on('-v', '--version')         { Migrator.output_version ; exit 0 }
     opts.on('-h', '--help')            { Migrator.output_help }
     opts.on('-s', '--serial SERIAL')   { |serial| @options.serial_root = serial}
     opts.on('-q', '--quiet')           { @options.verbose = false }
     opts.on('-l', '--level LEVEL')     { |level| @options.level = level }
     opts.on('-a', '--all')             { @options.all = true }
     opts.on('-o', '--only-new')        { @options.all = false }
     opts.on('-c', '--country COUNTRY') { |country| @default_country = country
                                  @default_country_id = get_country_id(@default_country)}
     opts.parse!(args)

    @logger = MyLogger.new(@options.level, 
                    File.join(MIGRATOR_ROOT, 'migrator-log'), 
                    File.join(MIGRATOR_ROOT, 'migrator-errors'), 
                    @options.verbose)
                    
    unless @options.serial_root
      puts "You must specify where the serial root is in the config file or with the --serial option"
      Process.exit!(1)
    end
    @stats = Statistic.new(File.join(MIGRATOR_ROOT,'migrator-stats'))

    if @options.verbose 
      puts "Country: #{@default_country}"
      puts "Logger level: #{@options.level}"
      puts "Serial: #{@options.serial_root}"
    end
  end

  def self.output_version
    puts "#{File.basename(__FILE__)} version #{VERSION}"
  end
  
  def self.output_help
    output_version
    RDoc::usage()
  end

  def run
    @logger.info("Pais Por Defecto: #{@options.default_country}")

    if File.directory? @options.serial_root
      begin
        
        if @options.all
          # Migrate all the files
          Dir.foreach(@options.serial_root) do |dir|
            next if dir =~ /^(\.|\..*|code|issue|issn|section|title|titleanterior)\.?$/
          
            full_dir = File.join(@options.serial_root, dir)
            @current_journal = dir
            process_journal(full_dir)
          end
        else
          # Migrate only the modified files
          # Check if serial is a git repo
          if File.exists? File.join(@options.serial_root, '.git')
            process_with_git()
          else
            @logger.error("Serial", "No es un repositorio Git")
            @logger.close
            @stats.close
            Process.exit!(1)
          end
        end
      ensure
        @logger.close
        @stats.close
      end
    else
      @logger.error("Directorio Raiz", "No existe el directorio raiz #{@options.serial_root}")
      @logger.close
      @stats.close
      Process.exit!(1)
    end
  end

  private

  def obtain_value(line)
    array = line.split(':')
    case array[0]
      when 'LOGGER'
                @options.level = array[1].strip.to_sym
      when 'SERIAL_ROOT'
                @options.serial_root = array[1].strip
      when 'COUNTRY'
                @default_country = array[1].strip
                @default_country_id = get_country_id(@default_country)
      else
                puts "Archivo de configuración ilegal (Opción #{array[0]} inválida)."
    end
  end

  def get_country_id(name)
    country = Country.find_by_name(name)

    #TODO: Lanzar un error si no se encuentra en la DB el pais requerido.
    country.id
  end

  def default_publisher
    publisher = Publisher.find_by_name("Unassigned")
    if publisher == nil
      publisher = Publisher.new
      publisher.name = "Unassigned"
      publisher.save
    end

    @logger.info("Editorial Por Defecto: Unassigned")

    publisher.id
  end

  def process_journal(journal_dir)
    # TODO: Obtener el publisher de la revista
    @current_publisher_id = default_publisher()
    @current_journal_id = nil

    if File.directory? journal_dir
      puts "Migrando la revista: " + @current_journal
      @logger.info("Migrando la revista: #{@current_journal}")
      @logger.pdf_report_journal(@current_journal)

      Dir.foreach(journal_dir) { |issue_dir|
        next if issue_dir =~ /^(\.|_notes|paginasinformativas)\.?$/

        full_dir = File.join(journal_dir, issue_dir)
        @current_issue_full_dir = full_dir
        @current_issue = issue_dir
        process_issue(full_dir)
      }
    else
      @logger.info( "La ruta #{journal_dir} no es un directorio")
    end
  end

  def process_issue(issue_dir)
    @logger.info("Migrando numero: " + @current_issue)
    if File.directory? issue_dir
      @current_journal_issue_id = nil
      #TODO: Encontrar una forma de obtener el título completo de la revista
      process_articles(issue_dir)
    end
  end

  def process_articles(issue_dir)
    article_dir = File.join(issue_dir, "markup")

    if File.directory? article_dir
      Dir.foreach(article_dir) { |article|
        next unless article =~ /^.*(\.)(txt)$/

        full_dir = File.join(article_dir, article)
        if File.file? full_dir
          @current_article = article.sub(/\.(txt)/, "")

          @logger.info("Procesando articulo: " + @current_article)
          process_article(full_dir)
        end
      }
    else
      @logger.error("El número #{File.basename(issue_dir)} de la revista #{@current_journal}",
                    "El numero no contiene archivos marcados.")
      @logger.pdf_report_error(File.basename(issue_dir),"El número no contiene archivos marcados")
    end
  end

  def process_article(marked_file)
    begin
      article = SgmlArticle.new(marked_file, @logger)
      if !@current_journal_id
        create_journal(article)
      end

      if !@current_journal_issue_id && @current_journal_id
        create_journal_issue(article)
      end

      if @current_journal_issue_id && @current_journal_id
        create_article(article)
      end
    rescue Exception => e
      @logger.pdf_report_error @current_article, e.message
    end

    #@logger.info("Lenguaje: #{article.language}, Titulo Revista: #{article.journal_title}")
    #@logger.info("Volumen: #{article.volume}, Numero: #{article.number}")
    #@logger.info("Año: #{article.year}, Revista ISSN: #{article.journal_issn}")
    #@logger.info("Primera pagina: #{article.fpage}, Ultima pagina: #{article.lpage}")
  end

  def create_journal(article)
    journal = Journal.new
    journal.title = article.journal_title
    journal.country_id = @default_country_id
    journal.publisher_id = @current_publisher_id
    journal.abbrev = article.journal_title
    journal.issn = article.journal_issn
    journal.incomplete = true

    @logger.info("Titulo de la Revista: #{journal.title}")
    @logger.info("ID del pais: #{journal.country.name}")
    @logger.info("ID del publicador: #{journal.publisher.name}")
    @logger.info("Abreviacion: #{journal.abbrev}")
    @logger.info("ISSN: #{journal.issn}")

    if journal.save
      @current_journal_id = journal.id
      @logger.info("Creando Revista ID: #{@current_journal_id}")
      @stats.add :journal
    else
      @logger.error_message("Error al crear la revista (SciELO)")
      journal.errors.each { |key, value|
        @logger.error("Revista #{@current_journal}", "#{key}: #{value}")
      }
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

    @logger.info("Numero: #{journal_issue.number}")
    @logger.info("Volumen: #{journal_issue.volume}")
    @logger.info("Volumen Suplemento: #{journal_issue.volume_supplement}")
    @logger.info("Numero Suplemento: #{journal_issue.number_supplement}")
    @logger.info("Año: #{journal_issue.year}")

    if journal_issue.save
      @current_journal_issue_id = journal_issue.id
      @logger.info( "Creando Numero de Revista ID: #{@current_journal_issue_id}")
      @stats.add :issue
    else
      @logger.error_message("Error al crear el número de la revista (SciELO)")
      journal_issue.errors.each { |key, value|
        @logger.error("Artículo #{@current_article} de la revista #{@current_journal}", "#{key}: #{value}")
      }
    end
  end

  def create_article(article)
    new_article = Article.create
    new_article.title = article.title.strip
    # new_article.subtitle = article.subtitle
    new_article.journal_issue_id = @current_journal_issue_id
    new_article.fpage = article.fpage
    new_article.lpage = article.lpage
    language = Language.find_by_code(article.language)
    if language
      new_article.language_id = language.id
    else
      @logger.error_message("No se encontró el lenguaje #{article.language} asociado al artículo")
    end

    @logger.info("Lenguaje: #{new_article.language.name}")
    @logger.info("Tituto Articulo: #{new_article.title}")
    @logger.info("Titulo Revista: #{new_article.journal_issue.journal.title}")
    @logger.info("Pagina inicial: #{new_article.fpage}, Pagina final: #{new_article.lpage}")

    if new_article.save
      @logger.info( "Creando Articulo ID: #{new_article.id}")
      @stats.add :article
      authors = AssociatedAuthors.new({
                                        :front => article.front,
                                        :id => new_article.id,
                                        :file => @current_article,
                                        :journal_name => @current_journal,
                                        :logger => @logger,
                                        :stats => @stats
                                      })

      #TODO: Si no hay autores no se crea las referencias asociadas al articulo.
      references = AssociatedReferences.new({
                                              :back => article.back,
                                              :cited_by_article_id => new_article.id,
                                              :country_id => @default_country_id,
                                              :publisher_id => @current_publisher_id,
                                              :logger => @logger,
                                              :article_file_name => @current_article,
                                              :journal_name => @current_journal,
                                              :stats => @stats
                                            })
      files = AssociatedFiles.new(
                                  :path => @options.serial_root,
                                  :journal => @current_journal,
                                  :issue => @current_issue,
                                  :article => @current_article,
                                  :logger => @logger,
                                  :id => new_article.id
                                  )
      
      begin
        authors.insert_authors()
      rescue ArgumentError
        @logger.error_message( 'El articulo no tiene autores.')
      end

      begin
        references.insert_references()
      rescue Exception => e
        @logger.pdf_report_error @current_article, e.message
      end
    else
      @logger.error_message("Error al crear el artículo (SciELO)")
      new_article.errors.each{ |key, value|
        if key == "title"
          @logger.error("Artículo #{@current_article} de la revista #{@current_journal}",
                        "El lenguaje del articulo difiere al lenguaje del titulo.")
          @logger.pdf_report_error(@current_article,"El lenguaje del artículo difiere al lenguaje del título")
        else
          @logger.error("Artículo #{@current_article} de la revista #{@current_journal}", "#{key}: #{value}")
        end
      }
    end
  end
  
  def process_with_git(options = [])
    # Find modified articles
    # TODO: 
    # * Specify the --since option for git
    articles = `cd #{@options.serial_root} && git --no-pager log --name-status -n 1 --pretty=format: -- */*/markup/*.txt`.strip.split(/\n+/)
    @current_publisher_id = default_publisher()

    articles.each do |output_line|
      begin
        status, file = output_line.split("\t")
        puts "#{status} - #{file}"
        sgml_article = SgmlArticle.new(File.join(@options.serial_root, file), @logger)

        journal = Journal.find_by_title(sgml_article.journal_title)

        data = file.split "/"
        data.delete_at 2
        data[2].gsub!(/\.txt/, "")
        
        @current_journal,@current_issue,@current_article = data

        #### Create Journal if needed
        if !journal
          create_journal(sgml_article)
        else
          @current_journal_id = journal.id
        end

        issue = JournalIssue.find_by_volume_and_number_and_volume_supplement(sgml_article.volume,
                                                                             sgml_article.number,
                                                                             sgml_article.volume_supplement)

        #### Create Issue if needed
        if !issue && @current_journal_id
          create_journal_issue(sgml_article)
        else
          @current_journal_issue_id = issue.id
        end
        
        if @current_journal_issue_id && @current_journal_id
          # We have boch the journal and the issue
          case status
          when 'A' # Added
            create_article(sgml_article)
          when 'M' # Modified
            sgml_article_path = File.join(@options.serial_root,
                @current_journal,
                @current_issue,
                "markup",
                "#{@current_article}.txt")
            assoc_file = AssociatedFile.find_by_sgml_path(sgml_article_path)
            article = assoc_file.article
            article.destroy if article
            create_article(sgml_article)
          end
          
        end

      rescue Exception => e
        @logger.pdf_report_error @current_article, e.message
      end

    end
  end

end

## Migración de Datos Scielo

Migrator.new(ARGV).run

