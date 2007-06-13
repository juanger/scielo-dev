class SgmlArticle
  attr_reader :language, :title, :volume, :number, :year, :fpage, :lpage, :issn

  def initialize(doc_path)
    if File.file? doc_path
      buffer = open(doc_path, "r") { |doc| doc.read }
      @document = Iconv.iconv("UTF-8", "ISO-8859-15", buffer).to_s
    end

    tag_article = /\[article.*?\]/m.match(@document).to_s

    if tag_article.empty?
      raise ArgumentError, "No es un documento de tipo article"
    end

    @language = get_language(tag_article)
    @title = get_title(tag_article)

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

  def get_language(article_tag)
    /(language=)(.{2})/.match(article_tag)[2].to_s
  end

  def get_title(article_tag)
    /(stitle=)"(.*)"/.match(article_tag)[2].to_s
  end
end

