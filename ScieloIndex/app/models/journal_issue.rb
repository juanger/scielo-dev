class JournalIssue < ActiveRecord::Base
  validates_presence_of :number, :volume, :year
  validates_numericality_of :year, :journal_id, :only_integer => true
  validates_inclusion_of :year, :in => (Date.today.year - 1000)..(Date.today.year + 1)

  belongs_to :journal
  has_many :articles, :through => :article_authors
  has_many :authors, :through => :article_authors
end
