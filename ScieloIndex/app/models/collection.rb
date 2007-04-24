class Collection < ActiveRecord::Base
  validates_inclusion_of :id, :in => 1..999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true 
  validates_inclusion_of :country_id, :in => 1..999, :allow_nil => true
  validates_inclusion_of :publisher_id, :in => 1..9999, :allow_nil => true
  validates_presence_of :title, :country_id, :publisher_id
  validates_length_of :title, :in => 3..999
  validates_length_of :state, :in => 2..200
  validates_length_of :url, :other, :city, :in => 2..200, :allow_nil => true
  #validates_length_of :email, :allow_nil => true
  validates_format_of :url, :email, :with => /^[-a-zA-Z0-9_\/.:@]*$/
  validates_format_of :other, :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ:;'",.&?!() ]*$/
end
