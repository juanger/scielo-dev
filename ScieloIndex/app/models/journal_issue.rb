class JournalIssue < ActiveRecord::Base
  validates_presence_of :year
  validates_numericality_of :year, :journal_id, :only_integer => true
  validates_inclusion_of :year, :in => (Date.today.year - 1000)..(Date.today.year + 1)

  belongs_to :journal
  has_many :articles

  validates_associated :journal, :articles
end
