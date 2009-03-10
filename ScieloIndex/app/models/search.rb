class Search
  include QueryHelper
  attr_accessor :author, :title, :title_phrase, :title_any, :since_year, :until_year, :journal_id, :page
  
  def initialize(search_terms, page, mode = :basic)
    
    case search_terms
    when String
      @author =  @title_any = search_terms
      @title = @title_phrase = ""
    when Hash
      search_terms.each do |key,val|
        instance_variable_set("@#{key}", val.strip)
      end
    end
    @page = page
    @mode = mode
  end
  
  def articles
    @collection ||= find_articles
  end
  
  def inspect
    "{author:#{author}, title:\"#{title}\", title_phrase:\"#{title_phrase}\", journal_id:#{journal_id}}"
  end
  
  private

  def find_articles
    Article.paginate(:all, :joins => joins, :conditions => conditions, :page => page,
     :select => select, :count => count, :per_page => per_page, :order => order)
  end

  def conditions
    [conditions_clauses.join(" #{mode("OR","AND")} "), conditions_values].flatten
  end
  
  def conditions_clauses
    conditions_parts.map { |condition| condition.first }
  end

  def conditions_values
    conditions_parts.map { |condition| condition.second }
  end

  # Each condition part is an array with two values: the clause and the value

  def conditions_parts
    private_methods(false).grep(/_conditions$/).map { |m| send(m) }.compact
  end

  def author_conditions
    if (terms = author.split).size <= 1
      return ["authors.lastname #{postgres? "ILIKE", "LIKE"} ?", "%#{author}%"] unless author.blank?
    else
      return ["authors.firstname LIKE ? AND authors.lastname LIKE ?", terms[0], terms[1]]
    end
  end

  def title_conditions
    ["articles.title = ?",  "#{title}"] unless title.blank?
  end

  def title_phrase_conditions
    ["articles.title LIKE ?",  "%#{title_phrase}%"] unless title_phrase.blank?
  end

  def title_any_conditions
    terms = title_any.split.map {|t| "%#{t}%"}
    conds = (["articles.title LIKE ?"]*terms.size).join(" OR ")
    ["(#{conds})", terms] unless title_any.blank?
  end
  
  def journal_conditions
    ["journal_issues.journal_id = ?",  "#{journal_id}"] unless journal_id.blank?
  end

  # def year_conditions
  #   ["products.price <= ?", ] unless year.blank?
  # end
  # 
  # def journal_id_conditions
  #   ["products.category_id = ?", category_id] unless journal_id.blank?
  # end
  
  def select
    'distinct articles.id, articles.title, '+
        'articles.subtitle, articles.journal_issue_id, tmp.citations, ' +
        'articles.fpage, articles.lpage, articles.language_id, files.filename, ' +
        'journal_issues.journal_id'
  end
  
  def joins
    " JOIN article_authors ON articles.id = article_authors.article_id " +
    "JOIN authors ON authors.id = article_authors.author_id " +
    "JOIN journal_issues ON articles.journal_issue_id = journal_issues.id " +
    "LEFT OUTER JOIN associated_files as files on articles.id = files.article_id " +
    "LEFT OUTER JOIN (select article_id ,count(article_id) as citations from citations " +
    "group by article_id) as tmp ON articles.id = tmp.article_id"
  end
  
  def count
    { :select => 'DISTINCT articles.title' }
  end
  
  def order
    "files.filename ASC #{postgres? "NULLS LAST"}," +
    "tmp.citations DESC #{postgres? "NULLS LAST"}"
  end
  
  def per_page
    10
  end
  
  def mode(basic, advanced)
    if @mode == :basic
      basic
    else
      advanced
    end
  end
  
end