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
      m_initials = self.middlename.split(' ').collect { |md| md.first.upcase }.to_s
    else
      m_initials = ""
    end

    [ self.lastname, self.firstname.first.upcase + m_initials ].join(' ')
  end

  def as_human
    [ self.degree.to_s, self.firstname, self.middlename.to_s, self.lastname ].join(' ')
  end

  def total_cites
    citesperart = self.articles.collect { |article| article.cites_number }
    citesperart.inject() { |sum,element| sum + element}
  end

  def Author.top_ten
    authors = Author.find(:all).collect { |author| 
      [author.id, author.as_human, author.total_cites]
    }
    all_authors = authors.sort { |one, other| other[2] <=> one[2] }.values_at(0..9)
    authors = Array.new
    all_authors.each { |author|
      authors << author unless author[2] == 0
    }
    
    authors
  end
  
  def info
    {:name => as_human, :citations => total_cites, :id => id}
  end
end
