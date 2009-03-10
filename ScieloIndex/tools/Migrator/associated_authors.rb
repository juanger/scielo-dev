class AssociatedAuthors

  def initialize(hash)
    @front = hash[:front]
    @article_id = hash[:id]
    @article_file_name = hash[:file]
    @logger = hash[:logger]
    @journal_name = hash[:journal_name]
    @stats = hash[:stats]
    match = /\[authgrp\](.*)\[\/authgrp\]/m.match(@front)

    if match
      @authgrp = match[1].to_s
    else
      raise ArgumentError, "El documento no tiene autores asociados."
    end
    @logger.debug("#{@authgrp}")
  end

  def insert_authors
    @authors = @authgrp.scan(/\[author.*?\](.*?)\[\/author\]/).flatten

    count = 1
    for author in @authors
      @logger.debug( "#{author}")
      author_hash = {
        :firstname => '',
        :middlename => '',
        :lastname => ''
      }
      match = /\[fname\](.*)\[\/fname\]/m.match(author)

      if match
        fname = match[1].to_s
        fname = fname.sub(/\n/, '')
      else
        author_hash.delete(:firstname)
      end

      match =  /\[surname\](.*)\[\/surname\]/m.match(author)
      if match
        sname = match[1].to_s
        sname = sname.sub(/\n/, '')
        array = sname.split(' ')
        sname = array.collect { |name| name.mb_chars.capitalize.to_s }.join(' ')
        author_hash[:lastname] = sname
      else
        author_hash.delete(:lastname)
      end

      names = fname.split(' ');

      if names.size() > 1
        fname = names[0]
        names.delete_at(0)
        mname = names.join(' ')
        author_hash[:firstname] = fname
        author_hash[:middlename] = mname
      else
        fname = names[0].to_s
        author_hash[:firstname] = fname
        author_hash.delete(:middlename)
      end

      search = Author.find(:first, :conditions => author_hash)
      if !(search.nil?)
        @logger.warning("El autor ya ha sido creado")

        create_association(search.id, count)
        count += 1
      else
        if !author_hash[:middlename]
          author_hash[:middlename] = ''
        end

        new_author = Author.new
        new_author.firstname = author_hash[:firstname]
        new_author.middlename = author_hash[:middlename]
        new_author.lastname = author_hash[:lastname]

        @logger.info( "Autor Nombre de Pila: #{new_author.firstname}")
        @logger.info( "Autor Nombres: #{new_author.middlename}")
        @logger.info( "Autor Apellidos: #{new_author.lastname}")



        if new_author.save
          @logger.info( "Creando autor #{new_author.id}")
          @stats.add :author

          create_association(new_author.id, count)
          count += 1
        else
          @logger.error_message("Error al crear un autor")
          new_author.errors.each{ |key, value|
            @logger.error("Artículo #{@article_file_name} de la revista #{@journal_name}", "#{key}: #{value}")
          }
        end
      end
    end
  end

  def create_association (author_id, order)
    article_author = ArticleAuthor.new
    article_author.article_id = @article_id
    article_author.author_id = author_id
    article_author.author_order = order

    @logger.info( "Creando asociacion articulo-autor")
    @logger.info( "ID Articulo: #{article_author.article_id}")
    @logger.info( "ID Autor: #{article_author.author_id}")
    @logger.info( "Orden: #{article_author.author_order}")

    if article_author.save
      @logger.info( "Creando articulo-autor #{article_author.id}")

    else
      @logger.error_message("Error al crear la relacion articulo-autor")
      @logger.error("Articulo #{@article_file_name}", "Se trato de insertar un mismo autor dos veces.")
    end
  end
end

