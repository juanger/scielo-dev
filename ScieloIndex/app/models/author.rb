class Author < ActiveRecord::Base
  validates_presence_of :firstname, :lastname
  validates_length_of :firstname, :lastname, :in => 3..30
  validates_length_of :middlename, :maximum  => 20, :allow_nil => true
  validates_length_of :suffix, :maximum => 8, :allow_nil => true
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_format_of :firstname, :lastname, :middlename, :suffix, :with => /^[-a-zA-ZáéíóúÁÉÍÓÚñÑ. ]*$/

  # FIXME: No funciona esta relacion con la tabla article.
  #has_many :articles, :through => :article_authors
  has_and_belongs_to_many :institutions, :join_table => :author_institutions
end
