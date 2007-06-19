class AssociatedAuthors

  def initialize(front, article_id)
    @front = front
    @article_id = article_id

    match = /\[authgrp\](.*)\[\/authgrp\]/m.match(@front)

    if match
      @authgrp = match[1].to_s
    else
      raise ArgumentError, "El documento no tiene autores asociados."
    end
    puts "DEBUG: #{@authgrp}"
  end

  def insert_authors
    @authors = @authgrp.scan(/\[author.*?\](.*?)\[\/author\]/).flatten

    count = 1
    for author in @authors
      puts "DEBUG: #{author}"
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
        fname = ''
      end

      match =  /\[surname\](.*)\[\/surname\]/m.match(author)
      if match
        sname = match[1].to_s
        sname = sname.sub(/\n/, '')
      else
        sname = ''
      end

      names = fname.split(' ');

      if names.size() > 1
        fname = names[0]
        names.delete_at(0)
        mname = names.join(' ')
      else
        fname = names[0].to_s
        mname = ''
      end

      author_hash[:firstname] = fname
      author_hash[:lastname] = sname
      author_hash[:middlename] = mname

      search = Author.find(:first, :conditions => author_hash)
      if !(search.nil?)
        puts "MSG: El autor ya ha sido creado"

        create_association(search.id, count)
        count += 1
      else
        new_author = Author.new
        new_author.firstname = author_hash[:firstname]
        new_author.middlename = author_hash[:middlename]
        new_author.lastname = author_hash[:lastname]

        puts "Autor Nombre de Pila: #{new_author.firstname}"
        puts "Autor Nombres: #{new_author.middlename}"
        puts "Autor Apellidos: #{new_author.lastname}"
        puts ""



        if new_author.save
          puts "Creando autor #{new_author.id}"
          puts ""

         create_association(new_author.id, count)
          count += 1
        else
          puts "Error: #{new_author.errors[:firstname].to_s}"
          puts "Error: #{new_author.errors[:lastname].to_s}"
        end
      end
    end
  end

  def create_association (author_id, order)
    article_author = ArticleAuthor.new
    article_author.article_id = @article_id
    article_author.author_id = author_id
    article_author.author_order = order

    puts "Creando asociacion articulo-autor"
    puts "ID Articulo: #{article_author.article_id}"
    puts "ID Autor: #{article_author.author_id}"
    puts "Orden: #{article_author.author_order}"

    if article_author.save
      puts "Creando articulo-autor #{article_author.id}"

    else
      puts "Error: #{article_author.errors[:article_id].to_s}"
      puts "Error: #{article_author.errors[:author_id].to_s}"
      puts "Error: #{article_author.errors[:author_order].to_s}"
    end
  end
end

