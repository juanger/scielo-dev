class JournalIssuesController < ScieloIndexController
  def initialize
    @model = JournalIssue
    @created_msg = 'The issue was created!'
    @updated_msg = 'The issue was updated!'
  end
end
