class AssociatedAuthors

  def initialize(front, article_id, logger)
    @front = front
    @article_id = article_id
    @logger = logger
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
        sname = array.collect { |name| name.chars.capitalize.to_s }.join(' ')
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

          create_association(new_author.id, count)
          count += 1
        else
          @logger.error( "Error: #{new_author.errors[:firstname].to_s}")
          @logger.error( "Error: #{new_author.errors[:lastname].to_s}")
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
      @logger.error( "Error: #{article_author.errors[:article_id].to_s}")
      @logger.error( "Error: #{article_author.errors[:author_id].to_s}")
      @logger.error( "Error: #{article_author.errors[:author_order].to_s}")
    end
  end
end

