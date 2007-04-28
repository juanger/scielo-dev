class AuthorInstitution < ActiveRecord::Base
  validates_presence_of :author_id, :institution_id
  validates_inclusion_of :author_id, :in => 1..9999
  validates_inclusion_of :institution_id, :in => 1..9999
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true, :only_integer => true
  validates_numericality_of :author_id, :institution_id, :only_integer => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :author_id, :scope => :institution_id

  belongs_to :author
  belongs_to :institution
end
