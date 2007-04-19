class Journal < ActiveRecord::Base
  validates_presence_of :title, :country_id, :publisher_id
  validates_length_of :title, :in => 3..100
  validates_length_of :state, :city, :maximum => 30, :allow_nil => true
  validates_length_of :url, :other, :maximum => 200, :allow_nil => true
  validates_length_of :email, :abbrev, :maximum => 20, :allow_nil => true
  validates_length_of :issn, :maximum => 9, :allow_nil => true
  validates_format_of :title, :state, :city, :with => /^[-a-zA-ZáéíóúÁÉÍÓÚñÑ,.& ]*$/
  validates_format_of :url, :email, :with => /^[-a-zA-Z0-9\/.:@]*$/
  validates_format_of :other, :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ:;'",.&?!() ]*$/
  validates_format_of :issn, :with => /^([0-9]{4}-[0-9]{3}[0-9X])?$/
  validates_inclusion_of :country_id, :in => 1..999
  validates_inclusion_of :publisher_id, :in => 1..9999
  validates_uniqueness_of :issn
end
