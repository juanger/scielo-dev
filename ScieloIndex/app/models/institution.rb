class Institution < ActiveRecord::Base
  validates_presence_of :name, :country_id
  validates_length_of :name, :within => 3..500
  validates_length_of :url, :abbrev, :state, :city, :maximum => 200, :allow_nil => true
  validates_length_of :zipcode, :phone, :fax, :maximum => 50, :allow_nil => true
  validates_length_of :other, :maximum => 100, :allow_nil => true
  validates_inclusion_of :country_id, :in => 1..999
  validates_inclusion_of :id, :in => 1..999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_inclusion_of :parent_id, :in => 1..999, :allow_nil => true
  validates_numericality_of :parent_id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :name, :scope => :country_id

  belongs_to :country
  has_and_belongs_to_many :authors, :join_table => :author_institutions
end
