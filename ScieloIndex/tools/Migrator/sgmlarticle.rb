class SgmlArticle
  attr_reader :language, :journal_title, :volume, :number, :supplement, :year, :fpage, :lpage, :journal_issn

  def initialize(doc_path)
    if File.file? doc_path
      buffer = open(doc_path, "r") { |doc| doc.read }
      @document = Iconv.iconv("UTF-8", "ISO-8859-15", buffer).to_s
    end

    @article_tag = /\[article.*?\]/m.match(@document).to_s

    if @article_tag.empty?
      raise ArgumentError, "No es un documento de tipo article"
    end

    @language = get_language()
    @journal_title = get_journal_title()
    @volume = get_volume()
    @number = get_number()
    @supplement = get_supplement()
    @year = get_year()
    @fpage = get_fpage()
    @lpage = get_lpage()
    @journal_issn = get_journal_issn()

    @front = /\[front\].*\[\/front\]/m.match(@document).to_s
    @body = /\[body\].*\[\/body\]/m.match(@document).to_s
    @back = /\[back\].*\[\/back\]/m.match(@document).to_s
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

  def get_supplement
    match = /(supplvol=)(\d+)/.match(@article_tag)
    if match
      match[2].to_s
    end
  end
end

