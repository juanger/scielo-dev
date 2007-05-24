class ArticleKeyword < ActiveRecord::Base
  validates_presence_of :article_id, :keyword_id
  validates_inclusion_of :article_id, :keyword_id, :in  => 1..99999
  validates_numericality_of :article_id, :keyword_id , :only_integer => true
  validates_inclusion_of :id, :in  => 1..9999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :article_id, :scope => :keyword_id

  belongs_to :article
  belongs_to :keyword

  validates_associated :article, :keyword
end
