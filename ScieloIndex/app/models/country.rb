class Country < ActiveRecord::Base
  #validates_numericality_of :id
  validates_format_of :id, :with => /^(\d){3}$/
  validates_presence_of :id,:name,:code 
  validates_length_of :name, :within => 3..80, :allow_nil => false
  validates_length_of :code, :within => 2..3, :allow_nil => false
  #validates_format_of :name, :code :with => /^[a-z]$/ 
  validates_uniqueness_of :name,:code, :scope => :id
end
