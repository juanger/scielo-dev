class Publisher < ActiveRecord::Base
  validates_presence_of :name
  validates_length_of :name, :in => 3..100
  validates_length_of :description, :maximum => 500, :allow_nil => true
  validates_length_of :url, :maximum => 200, :allow_nil => true
  validates_format_of :name, :with => /^[-a-zA-ZáéíóúÁÉÍÓÚñÑ,.&: ]*$/
  validates_format_of :description, :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ:;'",.&?!() ]*$/
  validates_format_of :url, :with => /^[-a-zA-Z0-9\/.:]*$/
  validates_numericality_of :id, :allow_nil => true, :only_integer => true, :greater_than_or_equal_to => 1
  validates_uniqueness_of :name
  
  has_many :collections
end
