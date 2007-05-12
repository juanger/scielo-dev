class Cite < ActiveRecord::Base
  validates_presence_of :cite_order, :article_id, :cited_by_article_id
  validates_uniqueness_of :article_id, :scope => :cited_by_article_id
  belongs_to :article
  belongs_to :cite, :class_name => "Article", :foreign_key => "cited_by_article_id"
end
