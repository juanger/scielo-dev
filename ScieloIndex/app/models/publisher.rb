class Publisher < ActiveRecord::Base
  validates_presence_of :name
  validates_length_of :name, :in => 3..100
  validates_length_of :descr, :maximum => 500, :allow_nil => true
  validates_length_of :url, :maximum => 100, :allow_nil => true
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_format_of :name, :with => /^[-a-zA-ZáéíóúÁÉÍÓÚñÑ,.&:() ]*$/
  validates_format_of :descr, :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ:;'",.&?!() ]*$/
  validates_format_of :url, :with => /^[-a-zA-Z0-9\/.:]*$/
  validates_uniqueness_of :name
end
