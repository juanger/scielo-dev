class AssociatedReferences

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

    @cite_number = 0 # número total de citas
    @cite_count = 0 # número de ocontrib
    
    for reference in references
      oiserial = nil
      ocontrib = nil
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

      # BUG algunos archivos con dos ocontrib
      match = /\[ocontrib\](.*?)\[\/ocontrib\]/.match(reference)
      @cite_number += 1
      if match && oiserial
        ocontrib = match[1].to_s
        @logger.debug("REF OCONTRIB: \n#{ocontrib}")
        if ocontrib
          @cite_count += 1
          create_other_article(ocontrib)
        end
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
      abbrev = match[1].strip.to_s
      journal_hash[:abbrev] = abbrev
    else
      journal_hash.delete(:abbrev)
    end

    if !journal_hash.empty?
      journal = Journal.find_by_title(journal_hash[:title], :first)
      if journal.nil?
        journal = Journal.find_by_abbrev(journal_hash[:abbrev], :first)
      end
    else
      journal = nil
    end

    @logger.debug("REF Journal Title: #{journal_hash[:title]}")
    @logger.debug("REF Journal Abbrev: #{journal_hash[:abbrev]}")
    if !(journal.nil?)
      @logger.info( "Se encontro el journal en la DB (Referencia): #{journal.id}")
      @current_journal_id = journal.id
      @current_journal = journal.title
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
        @stats.add :journal_ref
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
      #BUG: entra aqui?? journal_issue_hash nunca es empty por el journal_id
      journal_issue = nil
    end

    if !(journal_issue.nil?)
      @logger.info( "Se encontro el journal_issue en la DB (Referencia): #{journal_issue.id}")
      @current_journal_issue_id = journal_issue.id
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
      #puts match[1].to_s if !language

      article_hash[:language_id] = language.id

      if match[3]
        article_hash[:subtitle] = match[3].to_s
      else
        article_hash.delete :subtitle
      end

      # Determine if it already exists (find by title and subtitle)
      article = Article.find(:first, :conditions => {:title => article_hash[:title], :subtitle => article_hash[:subtitle]})
      # Determine if both where published in the same journal
      
      # Determine if both where published in the same volume

      # Merge data with the journal and journal issue

      

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
      @logger.error("El articulo de la referencia no tiene titulo")
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
                    :conditions => ["trim(both from LOWER(lastname)) LIKE ? AND" + # equal lastnames
                      " (trim(both from LOWER(firstname)) LIKE ? OR " + # equal firstames
                      # or equal initials
                      " lower(substring(firstname from 1 for 1) || substring(middlename from 1 for 1)) LIKE ? )",
                       last.chars.downcase.strip, first.chars.downcase, first.chars[0,2]]
        
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
          @logger.error "Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value}"
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

    cite = Cite.new cite_hash
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
