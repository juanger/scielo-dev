class Article < ActiveRecord::Base
  validates_presence_of :title
  validates_inclusion_of :id, :in => 1..999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_length_of :title, :within => 1..99999
  validates_length_of :fpage,  :within => 1..100
  validates_length_of :lpage,  :within => 1..100
  validates_length_of :page_range,  :within => 1..100
  validates_length_of :url, :within => 1..200
  validates_length_of :pacsnum, :within => 1..200
  validates_length_of :other,  :in => 2..100000

  has_and_belongs_to_many :journal_issues  , :join_table => 'article_authors'
  has_and_belongs_to_many :authors, :join_table => 'article_authors'
end
