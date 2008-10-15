class Search #< ActiveRecord::Base
  attr_accessor :author, :title, :title_phrase, :title_any, :since_year, :until_year, :journal_id, :page
  
  def initialize(hash, page)
    hash.each do |key,val|
      instance_variable_set("@#{key}", val.strip)
    end
    @page = page
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
    [conditions_clauses.join(' AND '), *conditions_options]
  end
  
  def conditions_clauses
    conditions_parts.map { |condition| condition.first }
  end

  def conditions_options
    conditions_parts.map { |condition| condition[1..-1] }.flatten
  end

  def conditions_parts
    private_methods(false).grep(/_conditions$/).map { |m| send(m) }.compact
  end

  def author_conditions
    if (terms = author.split).size <= 1
      return ["authors.lastname ~* ?", "#{author}"] unless author.blank?
    else
      return ["authors.firstname ~* ? AND authors.lastname ~* ?", terms[0], terms[1]]
    end
  end

  def title_conditions
    ["articles.title = ?",  "#{title}"] unless title.blank?
  end

  def title_phrase_conditions
    ["articles.title ~* ?",  "#{title_phrase}"] unless title_phrase.blank?
  end

  def title_any_conditions
    terms = title_any.split
    conds = terms.map {|t| "articles.title ~* ?" }.join(" OR ")
    ["(#{conds})",  *terms] unless title_any.blank?
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
    "LEFT OUTER JOIN (select article_id ,count(article_id) as citations from cites " +
    "group by article_id) as tmp ON articles.id = tmp.article_id"
  end
  
  def count
    { :select => 'DISTINCT articles.title' }
  end
  
  def order
    'files.filename ASC NULLS LAST ,tmp.citations DESC NULLS LAST'
  end
  
  def per_page
    10
  end
    
end