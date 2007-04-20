class ArticleAuthor < ActiveRecord::Base
  belongs_to :journal_issue
  belongs_to :article
  belongs_to :author
end
