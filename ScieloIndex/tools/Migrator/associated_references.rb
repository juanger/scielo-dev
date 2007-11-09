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
        create_other_journal_issue(oiserial)
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
        @current_journal = journal.title
        @logger.info( "Creando journal #{@current_journal_id}")
      else
        @logger.error_message("Error al crear el journal de la referencia")
        new_author.errors.each{ |key, value|
          @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value}")
        }
      end
    end
  end

  def create_other_journal_issue(serial)
    journal_issue_hash = {
      :journal_id => @current_journal_id,
      :volume => '',
      :number => '',
      :year => ''
    }

    match = /\[date dateiso="(.*?)"\].*?\[\/date\]/.match(serial)
    if match
      year = match[1].to_s
      journal_issue_hash[:year] = year.strip[0,4]
    else
      journal_issue_hash.delete(:year)
    end

    match = /\[volid\](.*?)\[\/volid\]/.match(serial)
    if match
      volume = match[1].to_s
      journal_issue_hash[:volume] = volume.strip
    else
      journal_issue_hash.delete(:volume)
    end

    match = /\[issueno\](.*?)\[\/issueno\]/.match(serial)
    if match
      number = match[1].to_s
      journal_issue_hash[:number] = number.strip
    else
      journal_issue_hash.delete(:number)
    end

    if !journal_issue_hash.empty?
      journal_issue = JournalIssue.find(:first, :conditions => journal_issue_hash)
    else
      journal_issue = nil
    end

    if !(journal_issue.nil?)
      @logger.info( "Se encontro el journal_issue en la DB: #{journal_issue.id}")
    else
      @logger.info( "No se encontro el journal_issue en la DB")

      if !journal_issue_hash[:year].nil?
        journal_issue_year = journal_issue_hash[:year]
        if !journal_issue_hash[:volume].nil?
          journal_issue_volume = journal_issue_hash[:volume]
        else
          journal_issue_volume = nil
        end

        if !journal_issue_hash[:number].nil?
          journal_issue_number = journal_issue_hash[:number]
        else
          journal_issue_number = nil
        end
      else
        #TODO: Crear un journal fantasma para agregar el articulo a el.
        raise ArgumentError
      end

      journal_issue = JournalIssue.new
      journal_issue.journal_id = @current_journal_id
      journal_issue.year = journal_issue_year
      journal_issue.volume = journal_issue_volume
      journal_issue.number = journal_issue_number

      @logger.info( "Creando Numero de Revista (Referencia)")
      @logger.info( "Titulo Revista: #{@current_journal}")
      @logger.info( "Volumen: #{journal_issue.volume}")
      @logger.info( "Numero: #{journal_issue.number}")
      @logger.info( "Año: #{journal_issue.year}")

      if journal_issue.save
        @current_journal_issue_id = journal_issue.id
        @logger.info( "Creando journal_issue #{@current_journal_issue_id}")
      else
        @logger.error_message("Error al crear el journal issue de la referencia")
        journal_issue.errors.each{ |key, value|
          @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value}")
        }
      end
    end
  end
end

