class Citation < ActiveRecord::Base
  validates_presence_of :article_id, :cited_by_article_id, :cite_order
  validates_inclusion_of :article_id, :cited_by_article_id, :cite_order, :in => 1..99999
  validates_numericality_of :article_id, :cited_by_article_id, :cite_order, :only_integer => true
  validates_inclusion_of :id, :in => 1..99999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :article_id, :scope => :cited_by_article_id

  belongs_to :article, :class_name => 'Article', :foreign_key => 'article_id'
  belongs_to :citation, :class_name => 'Article', :foreign_key => 'cited_by_article_id'
end
