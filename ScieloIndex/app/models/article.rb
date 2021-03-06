class Article < ActiveRecord::Base
  validates_presence_of :title, :journal_issue_id
  validates_length_of :title, :within => 1..99999
  validates_length_of :subtitle, :maximum => 99999, :allow_nil => true
  validates_length_of :fpage, :lpage, :page_range,  :maximum => 100, :allow_nil => true
  validates_length_of :url, :pacsnum, :other, :maximum => 500, :allow_nil => true
  validates_format_of :fpage, :lpage, :page_range,  :with => /^[-a-zA-Z0-9, ]*$/
  validates_inclusion_of :journal_issue_id, :in => 1..99999
  validates_numericality_of :journal_issue_id, :only_integer => true
  validates_inclusion_of :id, :in => 1..99999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :journal_issue_id, :scope => [:title, :subtitle]

  belongs_to :journal_issue
  belongs_to :language

  has_many :article_authors
  has_many :authors, :through => :article_authors,  :order => "article_authors.author_order ASC"

  has_many :article_keywords
  has_many :keywords, :through => :article_keywords

  has_many :article_subjects
  has_many :subjects, :through => :article_subjects

  has_many :article_citations, :foreign_key => :article_id, :class_name => 'Citation'
  has_many :citations, :through => :article_citations

  has_many :article_references, :foreign_key => :cited_by_article_id, :class_name => 'Citation'
  has_many :references, :through => :article_references, :source => :article

  has_many :alternate_titles
  has_many :titles, :through => :alternate_titles

  has_one :associated_file
  
  after_destroy :delete_associated_records

  def as_vancouver
    [ author_names_as_vancouver, title_as_vancouver, journal_as_vancouver].join('. ')
  end
    
  def author_names_as_vancouver
    authors.map { |author| author.as_vancouver}.join(', ')
  end

  def title_as_vancouver
    self.title.capitalize
  end

  def journal_as_vancouver
    self.journal_issue.journal.as_vancouver + ' ' + self.journal_issue.as_vancouver + ':' + self.fpage.to_s + '-' + self.lpage.to_s + '.'
  end
  
  def year
    self.journal_issue.year
  end
  
  private
  
  def delete_associated_records
    self.references.each do |ref|
      ref.destroy unless ref.citations.count >= 1
    end
    
    # This is not needed as we don't create alternate titles for now
    # self.titles.each do |title|
    #   title.destroy
    # end
    
    self.authors.each do |author|
      author.destroy unless author.articles.length >= 1
    end
    
    self.associated_file.destroy if associated_file
    
  end
  
end
