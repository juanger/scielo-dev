class AssociatedReferences

  include QueryHelper

  def initialize(hash)
    @back = hash[:back]
    @cited_by_article_id = hash[:cited_by_article_id]
    @country_id = hash[:country_id]
    @publisher_id = hash[:publisher_id]
    @logger = hash[:logger]
    @article_file_name = hash[:article_file_name]
    @journal_name = hash[:journal_name]
    @stats = hash[:stats]

    match_other = /\[other.*?\](.*)\[\/other\]/m.match(@back)
    match_vancouv = /\[vancouv.*?\](.*)\[\/vancouv\]/m.match(@back)
    if match_other
      @other = match_other[1]
    elsif match_vancouv
      @vancouv = match_vancouv[1]
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

    @cite_number = 0 # número total de citas
    @cite_count = 0 # número de ocontrib
    
    for reference in references
      oiserial = nil
      ocontrib = nil
      omonog = ""
      @cite_number += 1
      match = /\[oiserial\](.*)\[\/oiserial\]/.match(reference)
      if match
        oiserial = match[1]
        @logger.debug("REF OISERIAL: \n#{oiserial}")
      end
      # BUG algunos archivos con dos ocontrib
      match = /\[ocontrib\](.*?)\[\/ocontrib\]/.match(reference)
      if match
        ocontrib = match[1]
        @logger.debug("REF OCONTRIB: \n#{ocontrib}")
      end
      # puts "OISERIAL: " + oiserial
      if oiserial && ocontrib
        # The Following breaks the DTD, but sometimes the date is
        # outside oiserial and appears in ocontrib
        match = /\[date dateiso="(.*?)"\](.*?)\[\/date\]/.match(ocontrib)
        @year = match[2] if match
        begin
          create_other_journal(oiserial)
          create_other_journal_issue(oiserial)
          @cite_count += 1
          create_other_article(ocontrib)
        rescue Exception => e
          # Si hay una excepción, pásate a la siguiente referencia
          next
        end
      end

    end
  end

  def insert_vancouv_references()
    @logger.info( "Ingresando referencias de tipo vancouver")
  end

  def create_other_journal(serial)
    journal_hash = Hash.new

    if match = /\[sertitle\](.*)\[\/sertitle\]/.match(serial)
      title = match[1].to_s
      journal_hash[:title] = title
    end

    if match = /\[stitle\](.*)\[\/stitle\]/.match(serial)
      abbrev = match[1].strip.to_s
      journal_hash[:abbrev] = abbrev
    end

    unless journal_hash.empty?
      journal = Journal.find_by_title(journal_hash[:title], :first)
      unless journal
        journal = Journal.find_by_abbrev(journal_hash[:abbrev], :first)
      end
    end

    @logger.debug("REF Journal Title: #{journal_hash[:title]}")
    @logger.debug("REF Journal Abbrev: #{journal_hash[:abbrev]}")
    if journal
      @logger.info( "Se encontro el journal en la DB (Referencia): #{journal.id}")
      @current_journal_id = journal.id
      @current_journal = journal.title
    else
      @logger.info("No se encontro el journal en la DB")

      journal_title = journal_hash[:title] if journal_hash[:title]
      if journal_hash[:abbrev]
        journal_title ||= journal_hash[:abbrev]
        journal_abbrev = journal_hash[:abbrev]
      end

      if journal_hash.empty?
        #TODO: Crear un journal fantasma para agregar el articulo a el.
        raise ArgumentError.new("No se encontró el título ni la abreviación del journal")
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
        @stats.add :journal_ref
      else
        @logger.error_message("Error al crear el journal de la referencia")
        journal.errors.each{ |key, value|
          @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value}")
        }
      end
    end
  end

  def create_other_journal_issue(serial)
    journal_issue_hash = { :journal_id => @current_journal_id }

    if match = /\[date dateiso="(.*?)"\].*?\[\/date\]/.match(serial)
      year = match[1].to_s
      journal_issue_hash[:year] = year.strip[0,4]
    elsif @year
      journal_issue_hash[:year] = @year
    end

    if match = /\[date dateiso="(.*?)"\].*?\[\/date\]/.match(serial)
      volume = match[1].to_s
      journal_issue_hash[:volume] = volume.strip
    end

    if match = /\[issueno\](.*?)\[\/issueno\]/.match(serial)
      number = match[1].to_s
      journal_issue_hash[:number] = number.strip
    end

    # puts journal_issue_hash.inspect
    journal_issues = JournalIssue.find(:all, :conditions => journal_issue_hash)
    
    case journal_issues.size
    when 0
      journal_issue = nil
    when 1
      journal_issue = journal_issues.first
    else
      @logger.error("Condiciones insuficientes para encontrar un único journal_issue",
                    journal_issue_hash.inspect)
      raise Exception.new("Condiciones insuficientes para en contrar un único journal issue")
    end


    if !(journal_issue.nil?)
      @logger.info( "Se encontro el journal_issue en la DB (Referencia): #{journal_issue.id}")
      @current_journal_issue_id = journal_issue.id
    else
      @logger.info( "No se encontro el journal_issue en la DB")

      if journal_issue_hash[:year]
        journal_issue_year = journal_issue_hash[:year]
        if journal_issue_hash[:volume]
          journal_issue_volume = journal_issue_hash[:volume]
        else
          journal_issue_volume = nil
        end

        if journal_issue_hash[:number]
          journal_issue_number = journal_issue_hash[:number]
        else
          journal_issue_number = nil
        end
      else
        @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}, ocitat número #{@cite_number +1}",
                    "oiserial no contiene el año de publicación. Saltando cita...")
        raise Exception.new("oiserial no contiene el año de publicación. Saltando ocitat #{@cite_number + 1}...")
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
        @stats.add :issue_ref
      else
        @logger.error_message("Error al crear el journal issue de la referencia")
        journal_issue.errors.each{ |key, value|
          @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value}")
        }
      end
    end
  end

  def create_other_article(contrib)
    article_hash = {
      :title => '',
      :subtitle => '',
      :language_id => '',
      :journal_issue_id => @current_journal_issue_id
    }

    match = /\[title language=(.+)\](.+)\[\/title\](?:\[subtitle\](.+)\[\/subtitle\])?/.match(contrib)
    if match
      article_hash[:title] = match[2].to_s.strip
      language = Language.find_by_code(match[1].to_s)

      article_hash[:language_id] = language.id

      if match[3]
        article_hash[:subtitle] = match[3].to_s
      else
        article_hash.delete :subtitle
      end

      # Determine if it already exists (find by title and subtitle)
      article = Article.find(:first, :conditions => {:title => article_hash[:title], :subtitle => article_hash[:subtitle]})
      # TODO: Determine if both where published in the same journal
      # TODO: Determine if both where published in the same volume
        # TODO: Merge data with the journal and journal issue

      if article
        @logger.info( "Se encontro el articulo en la DB (Referencia): #{article.id}" )
        @current_article_id = article.id
        create_cite
      else
        @logger.info( "No se encontro el article en la DB")

        article = Article.new article_hash
        @logger.info( "Creando Articulo (Referencia)")
        @logger.info( "Titulo: #{article.title}")
        @logger.info( "Lenguaje: #{article.language.name}")
        @logger.info( "Journal: #{@current_journal}")

        if article.save
          @current_article_id = article.id
          @logger.info( "Creando articulo #{@current_article_id}")
          @stats.add :article_ref
          create_other_author(contrib)
          create_cite
        else
          @logger.error_message("Error al crear el artículo de la referencia")
          article.errors.each{ |key, value|
            @logger.error("Artículo Original #{@article_file_name} de la revista #{@journal_name}\n Articulo ref #{article_hash[:title]}", "#{key}: #{value}")
          }
        end
      end

    else
      @logger.error("Artículo Original #{@article_file_name} de la revista #{@journal_name}\n Articulo ref #{article_hash[:title]}","El articulo de la referencia no tiene titulo")
      article_hash.clear
      article = nil
    end
  end

  def create_other_author(contrib)
    author_hash = {
      :firstname => '',
      :lastname => '',
    }

    count = 1

    contrib.scan(/\[oauthor role=.+?\](?:\[surname\](.+?)\[\/surname\]|\[fname\](.+?)\[\/fname\])+?\[\/oauthor\]/) { |last, first|
      if last && first
        author_hash[:firstname] = first
        author_hash[:lastname] = last
        
        author = Author.find :first, 
                    :conditions => [
                      # equal lastnames
                      postgres?("trim(both from translate(LOWER(lastname),'ÁÉÍÓÚÑÜ','áéíóúñü')) LIKE ?","trim(LOWER(lastname)) LIKE ?") + " AND " +
                      # equal names of equal initials
                      postgres?("(trim(both from translate(LOWER(firstname),'ÁÉÍÓÚÑÜ','áéíóúñü')) LIKE ?","(trim(LOWER(firstname)) LIKE ?") + " OR " +
                      postgres?(" translate(lower(substring(firstname from 1 for 1) || substring(middlename from 1 for 1)),'ÁÉÍÓÚÑÜ','áéíóúñü')",
                                " lower(substr(firstname,1,1) || substr(middlename,1,1))") +
                                " LIKE ? )",
                       last.mb_chars.downcase.tr('ÁÉÍÓÚÑÜ','áéíóúü').strip, 
                       first.mb_chars.tr('ÁÉÍÓÚÑÜ','áéíóúü').downcase, 
                       first.mb_chars[0,2].tr('ÁÉÍÓÚÑÜ','áéíóúü').downcase
                    ]
        
        if author
          @logger.info "Se encontro el autor en la DB (Referencia): #{author.id}"
          create_association(author.id, count)
          count += 1
        else
          if !author_hash[:middlename]
              author_hash[:middlename] = ''
          end
          @logger.info "No se encontro el autor en la DB (referencia)"
          author = Author.new author_hash
          @logger.info( "Autor Nombre de Pila: #{author.firstname}")
          @logger.info( "Autor Apellidos: #{author.lastname}")
          if  author.save
            @logger.info "Creando autor #{author.id} (Referencia)"
            @stats.add :author_ref
            create_association(author.id,  count)
            count += 1
          else
            @logger.error_message("Error al crear el autor de la referencia")
            author.errors.each{ |key, error|
              @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{error} (#{author[key]})")
              @logger.pdf_report_error(@article_file_name,"#{key} inválido: #{author[key]} (en referencia #{@cite_number})")
            }
          end
        end
      end
    }
  end

  def create_association (author_id, order)
    article_author_hash = {
      :article_id => @current_article_id,
      :author_id => author_id,
      :author_order => order
    }

    article_author = ArticleAuthor.find :first, :conditions => article_author_hash
    if article_author
      @logger.info( "Se encontro la asociacion articulo-autor en la DB (Referencia): #{article_author.id}")
    else
      @logger.info( "No se encontro el article en la DB")

      article_author = ArticleAuthor.new
      article_author.article_id = @current_article_id
      article_author.author_id = author_id
      article_author.author_order = order

      @logger.info( "Creando asociacion articulo-autor (Referencia)")
      @logger.info( "ID Articulo: #{article_author.article_id}")
      @logger.info( "ID Autor: #{article_author.author_id}")
      @logger.info( "Orden: #{article_author.author_order}")

      if article_author.save
        @logger.info( "Creando articulo-autor #{article_author.id} (Referencia)" )
      else
        @logger.error_message("Error al crear la relacion articulo-autor (Referencia)")
        @logger.pdf_report_error(@article_file_name, "Se trato de insertar al autor #{Author.find(author_id).as_human} dos veces en la referencia #{@cite_number}.")
        article_author.errors.each do |key, value|
          @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value}")
        end
      end
    end
  end

  def create_cite
    cite_hash = {
      :article_id => @current_article_id,
      :cited_by_article_id => @cited_by_article_id,
      :cite_order => @cite_count
    }

    cite = Citation.new cite_hash
    if cite.save
      @logger.info "Creando cita #{cite.id}"
      @stats.add :cite
    else
      @logger.error_message("Error al crear la cita")
      cite.errors.each{ |key, value|
        @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value} (#{cite[key]})")
        @logger.pdf_report_error(@article_file_name,"artículo repetido: #{Article.find(@current_article_id).title} (en referencia #{@cite_number})")
      }
    end
  end
end
