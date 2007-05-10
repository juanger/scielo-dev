class Collection < ActiveRecord::Base
  validates_presence_of :title, :country_id, :publisher_id
  validates_length_of :title, :in => 3..100
  validates_length_of :state, :city, :maximum => 30, :allow_nil => true
  validates_length_of :url, :other, :maximum => 200, :allow_nil => true
  validates_length_of :email, :maximum => 20, :allow_nil => true
  validates_format_of :title, :state, :city, :with => /^[-a-zA-ZáéíóúÁÉÍÓÚñÑ:,.& ]*$/
  validates_format_of :url, :email, :with => /^[-a-zA-Z0-9\/.:@]*$/
  validates_format_of :other, :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ:;'",.&?!() ]*$/
  validates_inclusion_of :country_id, :in => 1..999
  validates_inclusion_of :publisher_id, :in => 1..9999
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true, :only_integer => true
  validates_numericality_of :country_id, :publisher_id, :only_integer => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true

  belongs_to :country
  belongs_to :publisher

  validates_associated :country, :publisher
end
