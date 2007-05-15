class Author < ActiveRecord::Base
  validates_presence_of :firstname, :lastname
  validates_length_of :firstname, :lastname, :in => 3..30
  validates_length_of :middlename, :maximum  => 100, :allow_nil => true
  validates_length_of :suffix, :maximum => 8, :allow_nil => true
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_format_of :firstname, :lastname, :middlename, :suffix, :with => /^[-a-zA-ZáéíóúÁÉÍÓÚñÑ. ]*$/

  has_many :article_authors
  has_many :articles, :through => :article_authors,
  :finder_sql => 'SELECT * FROM article_authors INNER JOIN articles ON articles.id = article_authors.article_id ' +
    'INNER JOIN journal_issues ON articles.journal_issue_id = journal_issues.id ' +
    'INNER JOIN journals ON journal_issues.journal_id = journals.id ' +
    'AND article_authors.author_id = #{id} ORDER BY journal_issues.year, ' +
    'journal_issues.volume, journal_issues.number, journals.title, articles.title DESC'

  has_many :author_institutions
  has_many :institutions, :through => :author_institutions

  def as_vancouver
    [ self.lastname,
      self.firstname.first.upcase + self.middlename.split(' ').collect { |md| md.first.upcase }.to_s
    ].join(' ')
  end
end
