# this script will be run by script/runner
$KCODE='u'
require 'jcode'
require 'iconv'

MIGRATOR_ROOT = RAILS_ROOT + '/tools/Migrator/'

require File.expand_path(MIGRATOR_ROOT + 'sgmlarticle')
require File.expand_path(MIGRATOR_ROOT + 'associated_authors')
require File.expand_path(MIGRATOR_ROOT + 'associated_references')
require File.expand_path(MIGRATOR_ROOT + 'associated_files')
require File.expand_path(MIGRATOR_ROOT + 'statistic')
require File.expand_path(MIGRATOR_ROOT + 'mylogger')


class Migrator
  def initialize
    config = MIGRATOR_ROOT + "config"
    if File.file? config
      open(config, "r") { |file|
        file.each { |line|
          next if line =~ /^(#.*)?$/
          obtain_value line}
      }
     @logger = MyLogger.new(@level, MIGRATOR_ROOT + 'migrator-log', MIGRATOR_ROOT + 'migrator-errors')
     @stats = Statistic.new(MIGRATOR_ROOT + 'migrator-stats')
    else
      @logger = MyLogger.new(:none)
      @logger.error("Configuración", "Por favor crear un archivo de configuracion con nombre config.")
      @logger.close
      Process.exit!(1)
    end
  end

  def process_scielo
    @logger.info("Pais Por Defecto: #{@default_country}")

    if File.directory? @serial_root
      begin
        Dir.foreach(@serial_root) { |dir|
          next if dir =~ /^(\.|code|issue|issn|section|title|titleanterior)\.?$/

          full_dir = File.join(@serial_root, dir)
          @current_journal = dir
          process_journal(full_dir)
        }
      ensure
        @logger.close
        @stats.close
      end
    else
      @logger.error("Directorio Raiz", "No existe el directorio raiz #{@serial_root}")
      @logger.close
      Process.exit!(1)
    end
  end

  private

  def obtain_value(line)
    array = line.split(':')
    case array[0]
      when 'LOGGER'
                @level = array[1].strip.to_sym
      when 'SERIAL_ROOT'
                @serial_root = array[1].strip
      when 'COUNTRY'
                @default_country = array[1].strip
                @default_country_id = get_country_id(@default_country)
      else
                @logger.warning("Archivo de configuración ilegal.")
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
      @logger.error("El número #{File.basename(issue_dir)} de la revista #{@current_journal}","El numero no contiene archivos marcados.")
    end
  end

  def process_article(marked_file)
    begin
      article = SgmlArticle.new(marked_file, @logger)
      #@logger.info("Lenguaje: #{article.language}, Titulo Revista: #{article.journal_title}")
      #@logger.info("Volumen: #{article.volume}, Numero: #{article.number}")
      #@logger.info("Año: #{article.year}, Revista ISSN: #{article.journal_issn}")
      #@logger.info("Primera pagina: #{article.fpage}, Ultima pagina: #{article.lpage}")

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
      @logger.warning("Archivo #{marked_file} no es un article.")
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
      @logger.error("No se encontró el lenguaje #{article.language} asociado al artículo")
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
      #TODO: create the associated files
      files = AssociatedFiles.new(
                                  :path => @serial_root,
                                  :journal => @current_journal,
                                  :issue => @current_issue,
                                  :article => @current_article,
                                  :id => new_article.id
                                  )
      
      begin
        authors.insert_authors()
        references.insert_references()
      rescue ArgumentError
        @logger.error( 'El articulo no tiene autores.')
      end
    else
      @logger.error_message("Error al crear el artículo (SciELO)")
      new_article.errors.each{ |key, value|
        if key == "title"
          @logger.error("Artículo #{@current_article} de la revista #{@current_journal}", "El lenguaje del articulo difiere al lenguaje del titulo.")
        else
          @logger.error("Artículo #{@current_article} de la revista #{@current_journal}", "#{key}: #{value}")
        end
      }
    end
  end
end

## Migración de Datos Scielo

migrator = Migrator.new()
migrator.process_scielo()

