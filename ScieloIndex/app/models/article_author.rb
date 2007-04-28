class ArticleAuthor < ActiveRecord::Base
  validates_presence_of :journal_issue_id, :article_id, :author_id
  validates_inclusion_of :journal_issue_id, :in => 1..9999
  validates_inclusion_of :article_id, :in => 1..9999
  validates_inclusion_of :author_id, :in => 1..9999
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true, :only_integer => true
  validates_numericality_of :journal_issue_id, :article_id, :author_id, :only_integer => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :journal_issue_id, :scope => [:article_id, :author_id]

  belongs_to :journal_issue
  belongs_to :article
  belongs_to :author
end
