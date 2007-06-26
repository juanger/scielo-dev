class SgmlArticle
  attr_reader :front, :body, :back, :title, :language, :journal_title, :volume, :number, :volume_supplement
  attr_reader :number_supplement, :year, :fpage, :lpage, :journal_issn

  def initialize(doc_path)
    if File.file? doc_path
      buffer = open(doc_path, "r") { |doc| doc.read }
      @document = Iconv.iconv("UTF-8", "ISO-8859-15", buffer).to_s
    end

    @article_tag = /\[article.*?\]/m.match(@document).to_s

    if @article_tag.empty?
      raise ArgumentError, "No es un documento de tipo article"
    end

    @front = /\[front\].*\[\/front\]/m.match(@document).to_s
    @body = /\[body\].*\[\/body\]/m.match(@document).to_s
    @back = /\[back\].*\[\/back\]/m.match(@document).to_s

    @title = get_title()

    @language = get_language()
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

  def get_title
    match = /(\[title.*\])(.*)(\[\/title\])/m.match(@front)
    if match
      titulo =  match[2].to_s
      if titulo.chars.upcase.to_s == titulo
        titulo.chars.capitalize.to_s
      else
        titulo
      end
    else
      ""
    end
  end

  def get_language
    match = /(language=)(.{2})/.match(@article_tag)
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

