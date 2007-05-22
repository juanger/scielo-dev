class Subject < ActiveRecord::Base
  validates_presence_of :name
  validates_length_of :name, :in => 3..500
  validates_inclusion_of :id, :parent_id, :in => 1..9999, :allow_nil => true
  validates_numericality_of :id, :parent_id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :name

  belongs_to :parent_subject, :class_name => "Subject", :foreign_key => "parent_id"
end
