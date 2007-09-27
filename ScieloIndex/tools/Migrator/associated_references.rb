class AssociatedReferences

  def initialize(back, cited_by_article_id, country_id, publisher_id, logger)
    @back = back
    @cited_by_article_id = cited_by_article_id
    @country_id = country_id
    @publisher_id = publisher_id
    @logger = logger

    match_other = /\[other.*?\](.*)\[\/other\]/m.match(@back)
    match_vancouv = /\[vancouv.*?\](.*)\[\/vancouv\]/m.match(@back)
    if match_other
      @other = match_other[1].to_s
      #puts "DEBUG REF #{@cited_by_article_id}: #{@other}"
    elsif match_vancouv
      @vancouv = match_vancouv[1].to_s
      #puts "DEBUG REF #{@cited_by_article_id}: #{@vancouv}"
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
      journal = Journal.find(:first, :conditions => journal_hash)
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
        @logger.error( "Error: #{journal.errors[:title].to_s}")
        @logger.error( "Error: #{journal.errors[:country_id].to_s}")
        @logger.error( "Error: #{journal.errors[:publisher_id].to_s}")
        @logger.error( "Error: #{journal.errors[:abbrev].to_s}")
        @logger.error( "Error: #{journal.errors[:issn].to_s}")
      end
    end
  end
end

