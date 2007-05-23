class Article < ActiveRecord::Base
  validates_presence_of :title, :journal_issue_id
  validates_length_of :title, :within => 1..99999
  validates_length_of :fpage, :lpage, :page_range,  :maximum => 100, :allow_nil => true
  validates_length_of :url, :pacsnum, :other, :maximum => 500, :allow_nil => true
  validates_format_of :fpage, :lpage, :page_range,  :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ:,.& ]*$/
  validates_inclusion_of :journal_issue_id, :in => 1..9999
  validates_numericality_of :journal_issue_id, :only_integer => true
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :journal_issue_id, :scope => :title

  belongs_to :journal_issue

  has_many :article_authors
  has_many :authors, :through => :article_authors,  :order => "article_authors.author_order ASC"

  has_many :cites
  has_one :associated_file

  def as_vancouver
    [ author_names_as_vancouver, title_as_vancouver, journal_as_vancouver].join('. ')
  end

  def cites_number
    self.cites.size
  end

  def author_names_as_vancouver
    limit = 6
    if self.authors.size < limit
      self.authors.collect { |author| author.as_vancouver }.join(', ')
    else
      (self.authors.values_at(0..(limit - 1)).collect { |author| author.as_vancouver }.join(', ') + [' et al.'])
    end
  end

  def title_as_vancouver
    self.title.capitalize
  end

  def journal_as_vancouver
    self.journal_issue.journal.as_vancouver + ' ' + self.journal_issue.as_vancouver + ':' + self.page_range + '.'
  end
end
