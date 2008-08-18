class SgmlArticle
  attr_reader :front, :body, :back, :title, :language, :journal_title, :volume, :number, :volume_supplement
  attr_reader :number_supplement, :year, :fpage, :lpage, :journal_issn

  def initialize(doc_path, logger)
    @logger = logger

    if File.file? doc_path
      buffer = open(doc_path, "r") { |doc| doc.read }
      @document = Iconv.iconv("UTF-8", "ISO-8859-15", buffer).to_s
    end

    @article_tag = /\[article.*?\]/m.match(@document).to_s

    if @article_tag.empty?
      raise ArgumentError, "No es un documento de tipo article"
    end

    @front = /\[front\].*\[\/front\]/m.match(@document).to_s
    @front.gsub!(/\[ign\].*?\[\/ign\]/m, '')
    @body = /\[body\].*\[\/body\]/m.match(@document).to_s
    @back = /\[back\].*\[\/back\]/m.match(@document).to_s
    @back.gsub!(/\[ign\].*?\[\/ign\]/m, '')

    @language = get_language()
    @titles = get_titles()
    #print_titles()

    @title = get_title()
    @subtitle = get_subtitle()

    @journal_title = get_journal_title()
    @volume = get_volume()
    @number = get_number()
    @volume_supplement = get_volume_supplement()
    @number_supplement = get_number_supplement()
    @year = get_year()
    @fpage = get_fpage()
    @lpage = get_lpage()
    @journal_issn = get_journal_issn()

  end

  def get_matches(regexp, part)
    case part
      when 'front'
                regexp.match(@front).to_s
      when 'body'
                regexp.match(@body).to_s
      when 'back'
                regexp.match(@body).to_s
    end
  end


  private

  def print_titles
    @titles.each { |language, title|
      puts 'Language '+ language + ' Title ' + title
    }
  end

  def get_titles
    match = /\[titlegrp\](.*)\[\/titlegrp\]/m.match(@front)

    if match
      titlegrp = match[1].to_s
      #puts "Titlegrp Block: #{titlegrp}"
      titles = []
      titlegrp.scan(/\[title language=([a-z]{2,3})\](.*?)\[\/title\]/m) { |match|
        language = $1
        temp_title = $2

        if temp_title.chars.upcase.to_s == temp_title
          title = temp_title.chars.capitalize.to_s
        else
          title = temp_title
        end

        #puts "Match: Language: #{language}, Title: #{title}"
        titles << [language, title]
      }

      #puts "Numero de titulos: #{titles.length}"
      titles
    else
      []
    end
  end

  def get_title
    article_title = ""
    titles = []
    @titles.each { |language, title|
      #puts "A#{@language}A"
      #puts "A#{language}A"
      if @language == language
        article_title = title
      else
        titles << [language, title]
      end
    }

    if article_title == "" and titles.length >= 1
      @logger.warning("Discrepancia entre el lenguage del articulo y el lenguage del titulo")
    end

    @titles = titles
    #puts "Titulo al finalizar get_title: #{article_title}"
    article_title
  end
  
  # def get_subtitle
  #   title = 
  # end

  def get_language
    match = /(language=)([a-z]{2,3})/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_journal_title
    match = /(stitle=)"(.*)"/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_volume
    match = /(volid=)(\d+)/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_number
    match = /(issueno=)(\d+)/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_year
    match = /(dateiso=)(\d{4})/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_fpage
    match = /(fpage=)(\d+)/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_lpage
    match = /(lpage=)(\d+)/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_journal_issn
    match = /(issn=)(\d{4}-\d{4})/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_volume_supplement
    match = /(supplvol=)(\d+)/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end

  def get_number_supplement
    match = /(supplno=)(\d+)/.match(@article_tag)
    if match
      match[2].to_s
    else
      ""
    end
  end
end

