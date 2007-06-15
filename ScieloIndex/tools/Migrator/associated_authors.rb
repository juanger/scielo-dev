class AssociatedAuthors

  def initialize(front)
    @front = front
    @authgrp = /\[authgrp\](.*)\[\/authgrp\]/m.match(@front)[1].to_s
    puts @authgrp
  end

  def insert_authors
    solo_authors = @authgrp.gsub(/\[ign\].*?\[\/ign\]/m, '')
    @authors = solo_authors.split("[/author]")
    puts @authors
    for author in @authors
      author_hash = {
        :firstname => '',
        :lastname => ''
      }
      match = /\[fname\](.*)\[\/fname\]/m.match(author)

      if match
        fname = match[1].to_s
      else
        fname = ''
      end

      match =  /\[surname\](.*)\[\/surname\]/m.match(author)
      if match
        sname = match[1].to_s
      else
        sname = ''
      end

      author_hash[:firstname] = fname.sub(/\n/, '')
      author_hash[:lastname] = sname.sub(/\n/, '')

      puts "Creando autor con Nombre: #{author_hash[:firstname]}"
      puts "Creando autor con Apellido: #{author_hash[:lastname]}"

      if !Author.find(:all, :conditions => author_hash).empty?
        puts 'MSG: El autor ya ha sido creado'
      else
        new_author = Author.new
        new_author.firstname = author_hash[:firstname]
        new_author.lastname = author_hash[:lastname]

        if new_author.save()
          puts "Creando autor #{new_author.id}"
        else
          puts "Error: #{new_author.errors[:firstname].to_s}"
          puts "Error: #{new_author.errors[:lastname].to_s}"
        end
      end
    end
  end
end

