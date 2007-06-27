class AssociatedReferences

  def initialize(back, cited_by_article_id)
    @back = back
    @cited_by_article_id = cited_by_article_id

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
    puts "Ingresando referencias de tipo other."
    references = @other.scan(/\[ocitat\](.*?)\[\/ocitat\]/).flatten

    count = 1
    for reference in references
      create_other_journal(reference)
    end
  end

  def insert_vancouv_references()
    puts "Ingresando referencias de tipo vancouver"
  end

  def create_other_journal(reference)
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
end

