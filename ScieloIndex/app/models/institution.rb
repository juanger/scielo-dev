class Institution < ActiveRecord::Base
  validates_presence_of :id, :name, :country_id
  validates_lenght_of :id, :in => 1..99999
  validates_lenght_of :name, :within => 3..500
  validates_inclusion_of :country_id, :in => 1..999
end
