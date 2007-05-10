class Author < ActiveRecord::Base
  validates_presence_of :firstname, :lastname
  validates_length_of :firstname, :lastname, :in => 3..30
  validates_length_of :middlename, :maximum  => 20, :allow_nil => true
  validates_length_of :suffix, :maximum => 8, :allow_nil => true
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_format_of :firstname, :lastname, :middlename, :suffix, :with => /^[-a-zA-ZáéíóúÁÉÍÓÚñÑ. ]*$/

  has_many :article_authors
  has_many :articles, :through => :article_authors
  # Queda pendiente agregar el siguiente query al has_many :articles, con el objetivo
  # de ofrecer la lista los artículos de forma ordenada
  # :include => [:journal_issue, :journal, :article],
  # :conditions => '',
  # :order => 'journal_issues.year, journal_issues.volume, journal_issues.number, articles.title, journals.title DESC'

  has_many :author_institutions
  has_many :institutions, :through => :author_institutions

  def name(style='vancouver') #Quizá este estilo nunca cambie :)
    author_name = [ self.lastname ]
    author_name << self.firstname.first.upcase + self.middlename.split(' ').collect { |md| md.first }.flatten.to_s
    author_name.join(' ')
  end
end
