class AssociatedReferences

  def initialize(hash)
    @back = hash[:back]
    @cited_by_article_id = hash[:cited_by_article_id]
    @country_id = hash[:country_id]
    @publisher_id = hash[:publisher_id]
    @logger = hash[:logger]

    match_other = /\[other.*?\](.*)\[\/other\]/m.match(@back)
    match_vancouv = /\[vancouv.*?\](.*)\[\/vancouv\]/m.match(@back)
    if match_other
      @other = match_other[1].to_s
      #puts "\nDEBUG REF #{@cited_by_article_id}: #{@other}\n"
    elsif match_vancouv
      @vancouv = match_vancouv[1].to_s
      #puts "DEBUG REF #{@cited_by_article_id}: #{@vancouv}"
    else
      # Caso cuando no es ni other ni vancouv la cita.
    end
  end

  def insert_references()
    if @other
      insert_other_references()
    elsif @vancouv
      insert_vancouv_references()
    end
  end

  private

  def insert_other_references()
    @logger.info( "Ingresando referencias de tipo other.")
    references = @other.scan(/\[ocitat\](.*?)\[\/ocitat\]/).flatten

    count = 1
    for reference in references
      oiserial = nil
      ocontrib = ""
      omonog = ""

      match = /\[oiserial\](.*)\[\/oiserial\]/.match(reference)
      if match
        oiserial = match[1].to_s
        @logger.debug("REF OISERIAL: \n#{oiserial}")
      end

      if oiserial
        create_other_journal(oiserial)
      end

    end
  end

  def insert_vancouv_references()
    @logger.info( "Ingresando referencias de tipo vancouver")
  end

  def create_other_journal(serial)
    journal_hash = {
      :title => '',
      :abbrev => ''
    }

    match = /\[sertitle\](.*)\[\/sertitle\]/.match(serial)
    if match
      title = match[1].to_s
      journal_hash[:title] = title
    else
      journal_hash.delete(:title)
    end

    match = /\[stitle\](.*)\[\/stitle\]/.match(serial)
    if match
      abbrev = match[1].to_s
      journal_hash[:abbrev] = abbrev
    else
      journal_hash.delete(:abbrev)
    end

    if !journal_hash.empty?
      journal = Journal.find_by_title(:first, journal_hash[:title])
      if journal.nil?
        journal = Journal.find_by_abbrev(:first, journal_hash[:abbrev])
      end
    else
      journal = nil
    end

    @logger.debug("REF Journal Title: #{journal_hash[:title]}")
    @logger.debug("REF Journal Abbrev: #{journal_hash[:abbrev]}")
    if !(journal.nil?)
      @logger.info( "Se encontro el journal en la DB: #{journal.id}")
    else
      @logger.info( "No se encontro el journal en la DB")

      if !journal_hash[:title].nil?
        journal_title = journal_hash[:title]
        if !journal_hash[:abbrev].nil?
          journal_abbrev = journal_hash[:abbrev]
        else
          journal_abbrev = nil
        end
      elsif !journal_hash[:abbrev].nil?
          journal_title = journal_hash[:abbrev]
          journal_abbrev = journal_hash[:abbrev]
      else
        #TODO: Crear un journal fantasma para agregar el articulo a el.
        raise ArgumentError
      end

      journal = Journal.new
      journal.title = journal_title
      journal.country_id = @country_id
      journal.publisher_id = @publisher_id
      journal.abbrev = journal_abbrev
      journal.incomplete = true
      journal.issn = nil

      @logger.info( "Creando Journal de referencia")
      @logger.info( "Titulo de la Revista: #{journal.title}")
      @logger.info( "ID del pais: #{journal.country_id}")
      @logger.info( "ID del publicador: #{journal.publisher_id}")
      @logger.info( "Abreviacion: #{journal.abbrev}")

      if journal.save
        @current_journal_id = journal.id
        @logger.info( "Creando journal #{@current_journal_id}")
      else
        @logger.error_message("Error al crear el journal de la referencia")
        new_author.errors.each{ |key, value|
          @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value}")
        }
      end
    end
  end
end

