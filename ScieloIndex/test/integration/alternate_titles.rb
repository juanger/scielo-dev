require "#{File.dirname(__FILE__)}/../test_helper"

class AlternateTitlesTest < ActionController::IntegrationTest
  fixtures :languages, :journals, :journal_issues, :articles, :alternate_titles, :users

  def setup
    @alternate_titles = [:alternate1, :alternate2, :alternate3]
    @myalternate_title = { :title => 'Implications of the Global Warming in the precipitation in the North Zone of Mexico', :language_id => 39, :article_id => 4}
    @session = 
  end
  
  def test_getting_index
     get "/alternate_titles"
     assert_equal 200, status
     assert_equal '/alternate_titles', path
  end

   def test_new
     get "/alternate_titles/new"
     assert_equal 200, status
     assert_equal '/alternate_titles/new', path
   end

   def test_showing
     @alternate_titles.each { | alternate_title |
       post "/alternate_titles/show", :id => alternate_titles(alternate_title).id
       assert 200, status
       assert_equal '/alternate_titles/show', path
     }
   end

   def test_editing
     @alternate_titles.each { | alternate_title |
       post "/alternate_titles/edit", :id => alternate_titles(alternate_title).id
       assert 200, status
       assert "/alternate_titles/edit/#{alternate_titles(alternate_title).id}", path
     }
   end

   def test_updating_name
     @alternate_titles.each { | alternate_title |
       post "/alternate_titles/update", :id => alternate_titles(alternate_title).id, :title => alternate_titles(alternate_title).title.reverse
       assert 302, status
       follow_redirect!
       assert "/alternate_titles/show/#{alternate_titles(alternate_title).id}", path
     }
   end

    def test_deleting
     @alternate_titles.each { | alternate_title |
       get "/alternate_titles/destroy", :id => alternate_titles(alternate_title).id
       assert 302, status
       assert '/alternate_titles/list',  path
     }
   end
end
