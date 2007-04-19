class Institution < ActiveRecord::Base
  validates_presence_of :id, :name, :country_id
  validates_length_of :id, :in => 1..99999
  validates_length_of :name, :within => 3..500
  validates_length_of :url, :within => 1..200
  validates_length_of :abbrev, :within => 1..200
  validates_length_of :state, :in => 2..200
  validates_length_of :city, :in => 2..200
  validates_length_of :zipcode, :in => 2..50
  validates_length_of :phone, :in => 2..50
  validates_length_of :fax, :in => 2..50
  validates_length_of :other, :in => 2..100
  validates_inclusion_of :country_id, :in => 1..999
  validates_uniqueness_of :name, :scope => :country_id
end
