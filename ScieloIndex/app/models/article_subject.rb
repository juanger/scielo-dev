class ArticleSubject < ActiveRecord::Base
  validates_presence_of :article_id, :subject_id
  validates_inclusion_of :article_id, :subject_id, :in  => 1..99999
  validates_numericality_of :article_id, :subject_id , :only_integer => true
  validates_inclusion_of :id, :in  => 1..99999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :article_id, :scope => :subject_id

  belongs_to :article
  belongs_to :subject

  validates_associated :article, :subject
end
