class Article < ActiveRecord::Base
  validates_presence_of :title
  validates_inclusion_of :id, :in => 1..999, :allow_nil => true 
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_length_of :title, :within => 1..99999
  validates_length_of :pages, :within => 1..100
  validates_length_of :url, :within => 1..200
  validates_length_of :pacsnum, :within => 1..200
  validates_length_of :other, :in => 2..100000

  belongs_to :journal_issue
  has_many :authors, :through => :article_authors
end
