class Author < ActiveRecord::Base
  validates_presence_of :firstname, :lastname
  validates_length_of :firstname, :lastname, :in => 1..30
  validates_length_of :middlename, :maximum  => 100, :allow_nil => true
  validates_length_of :suffix, :prefix, :degree, :maximum => 10, :allow_nil => true
  validates_inclusion_of :id, :in => 1..99999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_format_of :firstname, :lastname, :middlename, :prefix, :degree, :with => /^[^\t\r\n\f\d]*$/
  validates_format_of :suffix, :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ.]*$/
  validates_uniqueness_of :lastname, :scope => [:firstname, :middlename, :suffix]

  has_many :article_authors
  has_many :articles, :through => :article_authors,
  :finder_sql => 'SELECT * FROM article_authors INNER JOIN articles ON articles.id = article_authors.article_id ' +
    'INNER JOIN journal_issues ON articles.journal_issue_id = journal_issues.id ' +
    'INNER JOIN journals ON journal_issues.journal_id = journals.id ' +
    'AND article_authors.author_id = #{id} ORDER BY journal_issues.year, ' +
    'journal_issues.volume, journal_issues.number, journals.title, articles.title DESC'

  has_many :author_institutions
  has_many :institutions, :through => :author_institutions


  # def to_param
  #   "#{id}-#{lastname}-#{firstname}"
  # end

  def as_vancouver
    if self.middlename
      m_initials = self.middlename.mb_chars.split(' ').collect { |md| md.first.upcase }.to_s
    else
      m_initials = ""
    end
    
    if self.firstname.size > 2
      f_initials = self.firstname.mb_chars.first.upcase
    else
      f_initials = self.firstname.mb_chars.upcase
    end
    
    lastname_cap = self.lastname.mb_chars.split.map! {|part| part.capitalize}.join(' ')

    [ lastname_cap, f_initials + m_initials ].join(' ')
  end

  def as_human
    lastname_as_human = self.lastname.mb_chars.split.map! {|part| part.capitalize}.join(' ')
    [ self.degree.to_s, self.firstname, self.middlename.to_s, lastname_as_human ].join(' ')
  end

  def self_citations
    Citation.count(:joins => "JOIN article_authors ON cited_by_article_id = article_authors.article_id " +
                              "JOIN article_authors as cited_author ON cited_author.article_id = citations.article_id",
                    :conditions => ["article_authors.author_id = cited_author.author_id AND article_authors.author_id = ?", self.id])
  end

  def Author.top_ten
    authors = Author.find(:all).collect { |author| 
      [author.id, author.as_human, author.total_citations]
    }
    all_authors = authors.sort { |one, other| other[2] <=> one[2] }.values_at(0..9)
    authors = Array.new
    all_authors.each { |author|
      authors << author unless author[2] == 0
    }
    
    authors
  end
  
  def info
    {:name => as_human, :citations => total_citations, :id => id}
  end
end
