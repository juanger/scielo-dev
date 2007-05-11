class Cite < ActiveRecord::Base
  belongs_to :article
  belongs_to :cite, :class_name => "Article", :foreign_key => "cited_by_article_id"
end
