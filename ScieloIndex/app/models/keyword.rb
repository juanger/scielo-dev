class Keyword < ActiveRecord::Base
  validates_presence_of :name
  validates_length_of :name, :in => 3..500
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :name

  has_many :article_keywords
  has_many :articles, :through => :article_keywords
end
