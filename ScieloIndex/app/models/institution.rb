class Institution < ActiveRecord::Base
  validates_presence_of :name, :country_id
  validates_length_of :name, :within => 3..500
  validates_length_of :url, :abbrev, :address, :state, :city, :maximum => 200, :allow_nil => true
  validates_length_of :zipcode, :phone, :fax, :maximum => 50, :allow_nil => true
  validates_length_of :other, :maximum => 100, :allow_nil => true
  validates_inclusion_of :id, :parent_id, :in => 1..9999, :allow_nil => true
  validates_inclusion_of :country_id, :in => 1..999
  validates_numericality_of :id, :parent_id, :allow_nil => true, :only_integer => true
  validates_numericality_of :country_id, :only_integer => true
  validates_uniqueness_of :name, :scope => :country_id

  belongs_to :country
  belongs_to :parent_institution, :class_name => "Institution", :foreign_key => "parent_id"

  has_many :author_institutions
  has_many :authors, :through => :author_institutions
  
  validates_associated :country
end
