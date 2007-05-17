require "#{File.dirname(__FILE__)}/../test_helper"

class JournalIssuesTest < ActionController::IntegrationTest
  fixtures :journal_issues

  def setup
    @journal_issues = [:atm19_1, :csalud1_10]
  end

   def test_getting_index
     get "/journal_issues"
     assert_equal 200, status
     assert_equal '/journal_issues', path
   end

   def test_new
     get "/journal_issues/new"
     assert_equal 200, status
     assert_equal '/journal_issues/new', path
   end

   
   def  test_creating_new_journal_issue
     post "journal_issues/create", :record =>  {:id => 1, :journal_id => 3, :number => '19', :volume => '1', :year => 2006}
     assert_equal 302, status
     follow_redirect!
     assert_equal '/journal_issues/list', path
   end

   def test_showing
     @journal_issues.each { | journal_issue |
       post "/journal_issues/show", :id => journal_issues(journal_issue).id
       assert 200, status
       assert_equal '/journal_issues/show', path
     }
   end

   def test_editing
     @journal_issues.each { | journal_issue |
       post "/journal_issues/edit", :id => journal_issues(journal_issue).id
       assert 200, status
       assert "/journal_issues/edit/#{journal_issues(journal_issue).id}", path
     }
   end

   def test_updating_name
     @journal_issues.each { | journal_issue |
       post "/journal_issues/update", :id => journal_issues(journal_issue).id, :journal_id => journal_issues(journal_issue).journal_id+1, :number => journal_issues(journal_issue).number.reverse, :volume => journal_issues(journal_issue).volume.reverse, :year => journal_issues(journal_issue).year+1
       assert 302, status
       follow_redirect!
       assert "/journal_issues/show/#{journal_issues(journal_issue).id}", path
     }
   end

    def test_deleting
     @journal_issues.each { | journal_issue |
       get "/journal_issues/destroy", :id => journal_issues(journal_issue).id
       assert 302, status
       assert '/journal_issues/list',  path
     }
   end
end
