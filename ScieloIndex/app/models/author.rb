class Author < ActiveRecord::Base
  validates_presence_of :firstname, :lastname
  validates_length_of :firstname, :lastname, :in => 3..30
  validates_length_of :middlename, :maximum  => 20, :allow_nil => true
  validates_length_of :suffix, :maximum => 8, :allow_nil => true
  validates_inclusion_of :id, :in => 1..999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_format_of :firstname, :lastname, :middlename, :suffix, :with => /^[-a-zA-ZáéíóúÁÉÍÓÚñÑ. ]*$/
end
