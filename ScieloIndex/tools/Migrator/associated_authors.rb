class AssociatedAuthors

  def initialize(front)
    @front = front

    match = /\[authgrp\](.*)\[\/authgrp\]/m.match(@front)

    if match
      @authgrp = match[1].to_s
    else
      raise ArgumentError, "El documento no tiene autores asociados."
    end
    puts @authgrp
  end

  def insert_authors
    solo_authors = @authgrp.gsub(/\[ign\].*?\[\/ign\]/m, '')
    @authors = solo_authors.split("[/author]")
    puts @authors
    for author in @authors
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
        if new_fname = /([[:upper:]])([[:upper:]].*)/.match(fname)
          fname = new_fname[1].to_s
          mname = new_fname[2].to_s
        else
          mname = ''
        end
      end

      author_hash[:firstname] = fname
      author_hash[:lastname] = sname
      author_hash[:middlename] = mname

      puts "Creando autor con Nombre: #{author_hash[:firstname]}"
      puts "Creando autor con Apellido: #{author_hash[:lastname]}"

      if !Author.find(:all, :conditions => author_hash).empty?
        puts 'MSG: El autor ya ha sido creado'
      else
        new_author = Author.new
        new_author.firstname = author_hash[:firstname]
        new_author.middlename = author_hash[:middlename]
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

