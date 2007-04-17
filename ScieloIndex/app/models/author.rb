class Author < ActiveRecord::Base
  validates_presence_of :firstname, :lastname
  validates_length_of :firstname, :in => 3..30
  validates_length_of :middlename, :in => 2..30
  validates_length_of :lastname, :in => 3..30
  validates_length_of :suffix, :in => 3..4
end
