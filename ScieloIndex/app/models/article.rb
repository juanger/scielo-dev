class Article < ActiveRecord::Base
  validates_presence_of :title, :journal_issue_id
  validates_length_of :title, :within => 1..99999
  validates_length_of :fpage,  :within => 1..100
  validates_length_of :lpage,  :within => 1..100
  validates_length_of :page_range,  :within => 1..100
  validates_length_of :url, :within => 1..200
  validates_length_of :pacsnum, :within => 1..200
  validates_length_of :other,  :in => 2..100000
  validates_inclusion_of :journal_issue_id, :in => 1..9999
  validates_numericality_of :journal_issue_id, :only_integer => true
  validates_inclusion_of :id, :in => 1..999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true

  validates_uniqueness_of :journal_issue_id, :scope => :title

  belongs_to :journal_issue

  has_many :article_authors
  has_many :authors, :through => :article_authors,  :order => "article_authors.author_order ASC"

  # Quizás un artículo puede aparecer en diferente journals,
  # como esto es *incierto* lo dejaremos comentado, nomás pa' meter mas ruido
  # has_many :journal_issues
  # has_many :journals, :through => :journal_issues

  def journal
    info = [ self.journal_issue.journal.title ]
    if self.journal_issue.volume != nil and self.journal_issue.number != nil
      info << "#{self.journal_issue.volume}(#{self.journal_issue.number})"
    else
      info << self.journal_issue.volume if self.journal_issue.volume != nil
      info << self.journal_issue.number if self.journal_issue.number != nil
    end
    info << self.journal_issue.year
    info.join(', ')
  end
end
