class Country < ActiveRecord::Base
  validates_presence_of :id, :name, :code 
  validates_inclusion_of :id, :in => 1..999
  validates_length_of :name, :within => 3..80
  validates_length_of :code, :within => 2..3
  validates_numericality_of :id, :only_integer => true
  validates_format_of :name, :with => /^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{3,80}$/
  validates_format_of :code, :with => /^[a-zA-Z]{2,3}[ ]?$/
  validates_uniqueness_of :name, :code, :id
end
